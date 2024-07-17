using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.Xml;

namespace Wire.Props;

public sealed class XmlProps : IProps
{
    private readonly ScalarOf<IXML> xml;

    public XmlProps(string path)
        : this(new Uri(path))
    {
    }

    public XmlProps(Uri uri)
        : this(new InputOf(uri))
    {
    }

    public XmlProps(IInput input)
        : this(new ScalarOf<IXML>(() => new XMLCursor(input)))
    {
    }

    public XmlProps(ScalarOf<IXML> xml)
    {
        this.xml = xml;
    }

    public string Value(string prop)
    {
        return this.xml.Value().Values(Text(prop))[0];
    }

    public string Value(string prop, string defaults)
    {
        return new Ternary<bool, string>(
            new ScalarOf<bool>(() => this.Has(prop)),
            new ScalarOf<string>(() => this.xml.Value().Values(Text(prop))[0]),
            new ScalarOf<string>(() => defaults)
        ).Value();
    }

    public IEnumerable<string> Values(string prop)
    {
        return this.xml.Value().Values(Text(prop));
    }

    public bool Has(string prop)
    {
        if (string.IsNullOrEmpty(prop))
        {
            return false;
        }

        var path = $"//{prop}";
        return this.xml.Value().Nodes(path).Count > 0;
    }

    private string Text(string prop)
    {
        return $"//{prop}/text()";
    }
}