using Yaapii.Atoms;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Wire.Props;

public sealed class AppProps : IProps
{
    private readonly SolidList<IProps> properties;

    public AppProps(params string[] args)
        : this(
            new InputsFromFileNames(
                new PropertyFileNames("app", new ProfileNames(args))
            )
        )
    {
    }

    public AppProps(IEnumerable<IInput> inputs)
    {
        this.properties = new SolidList<IProps>(
            new Mapped<IInput, IProps>(
                input => new BasicProps(input),
                inputs
            )
        );
    }

    public string Value(string prop)
    {
        return new Ternary<bool, string>(
            new ScalarOf<bool>(() => this.Has(prop)),
            new ScalarOf<string>(() => this.Value(prop)),
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