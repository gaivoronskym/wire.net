using System.Reflection;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;
using PropsAsString = Yaapii.Atoms.List.Mapped<Wire.IProps, string>;
using InputAsString = Yaapii.Atoms.List.Mapped<Yaapii.Atoms.IInput, Wire.IProps>;

namespace Wire.Props;

public sealed class AppProps : IProps
{
    private readonly SolidList<IProps> properties;

    public AppProps(Assembly assembly, params string[] args)
        : this(
            new InputsFromFileNames(
                new PropertyFileNames("app", new ProfileNames(args)),
                assembly
            )
        )
    {
    }

    public AppProps(IEnumerable<IInput> inputs)
    {
        this.properties = new SolidList<IProps>(
            new InputAsString(
                input => new BasicProps(input),
                inputs
            )
        );
    }

    public string Value(string prop)
    {
        var values = new ListOf<string>(
            new PropsAsString(
                (input) => input.Value(prop),
                new Filtered<IProps>(
                    (input) => input.Has(prop),
                    this.properties
                )
            )
        );

        return new Ternary<bool, string>(
            new ScalarOf<bool>(() => this.Has(prop)),
            new ScalarOf<string>(() => values[^1]),
            new ScalarOf<string>(() => throw new IOException($"Property '{prop}' not found"))
        ).Value();
    }

    public string Value(string prop, string defaults)
    {
        return new Ternary<bool, string>(
            new ScalarOf<bool>(() => this.Has(prop)),
            new ScalarOf<string>(() => this.Value(prop)),
            new ScalarOf<string>(() => defaults)
        ).Value();
    }

    public IEnumerable<string> Values(string prop)
    {
        return new ListOf<string>(new Split(this.Value(prop), ","));
    }

    public bool Has(string prop)
    {
        return new Or<IProps>(
            props => props.Has(prop),
            properties
        ).Value();
    }
}