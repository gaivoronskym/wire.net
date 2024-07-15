using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.Xml;

namespace Wire.Props;

public sealed class XmlProps : IProps
{
    private readonly Solid<IXML> xml;

    public XmlProps(string path)
        : this(new Uri(path))
    {
    }

    public XmlProps(Uri uri)
        : this(new InputOf(uri))
    {
    }

    public XmlProps(IInput input)
        : this(new Solid<IXML>(() => new XMLCursor(input)))
    {
    }

    public XmlProps(Solid<IXML> xml)
    {
        this.xml = xml;
    }

    public string Value(string prop)
    {
        return new Ternary<bool, string>(
            new ScalarOf<bool>(() => this.Has(prop)),
            new ScalarOf<string>(() => this.xml.Value().Values(prop)[0]),
            new ScalarOf<string>(() => throw new ArgumentException($"Property '{prop}' does not exist in props"))
        ).Value();
    }

    public string Value(string prop, string defaults)
    {
        return new Ternary<bool, string>(
            new ScalarOf<bool>(() => this.Has(prop)),
            new ScalarOf<string>(() => this.xml.Value().Values(prop)[0]),
            new ScalarOf<string>(() => defaults)
        ).Value();
    }

    public IEnumerable<string> Values(string prop)
    {
        return new Mapped<IXML, string>(
            input => input.ToString()!,
            this.xml.Value().Nodes(prop)
        );
    }

    public bool Has(string prop)
    {
        return this.xml.Value().Nodes(prop).Count > 0;
    }
}