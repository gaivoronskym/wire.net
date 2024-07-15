using Yaapii.Atoms;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Wire.Props;

public sealed class BasicProps : IProps
{
    private static IFunc<IInput, Properties> Singleton =
        new StickyFunc<IInput, Properties>(input => new Properties(input));

    private readonly IInput input;

    public BasicProps(string path)
        : this(new Uri(path))
    {
    }

    public BasicProps(Uri uri)
        : this(new InputOf(uri))
    {
    }

    public BasicProps(IInput input)
    {
        this.input = input;
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
        return this.Props().ContainsKey(prop);
    }

    private Properties Props()
    {
        return BasicProps.Singleton.Invoke(this.input);
    }
}