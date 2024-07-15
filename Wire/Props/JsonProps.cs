using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.JSON;

namespace Wire.Props;

public sealed class JsonProps : IProps
{
    private readonly Solid<IJSON> json;

    public JsonProps(string path)
        : this(new Uri(path))
    {
    }

    public JsonProps(Uri uri)
        : this(new InputOf(uri))
    {
    }

    public JsonProps(IInput input)
        : this(new Solid<IJSON>(() => new JSONOf(input)))
    {
    }

    public JsonProps(Solid<IJSON> json)
    {
        this.json = json;
    }

    public string Value(string prop)
    {
        return new Ternary<bool, string>(
            new ScalarOf<bool>(() => this.Has(prop)),
            new ScalarOf<string>(() => this.json.Value().Values(prop)[0]),
            new ScalarOf<string>(() => throw new ArgumentException($"Property '{prop}' does not exist in props"))
        ).Value();
    }

    public string Value(string prop, string defaults)
    {
        return new Ternary<bool, string>(
            new ScalarOf<bool>(() => this.Has(prop)),
            new ScalarOf<string>(() => this.json.Value().Values(prop)[0]),
            new ScalarOf<string>(() => defaults)
        ).Value();
    }

    public IEnumerable<string> Values(string prop)
    {
        return new Mapped<IJSON, string>(
            input => input.ToString()!,
            this.json.Value().Nodes(prop)
        );
    }

    public bool Has(string prop)
    {
        return this.json.Value().Nodes(prop).Count > 0;
    }
}