using Yaapii.Atoms.List;

namespace Wire.Tests.Fk;

public sealed class FkProps : IProps
{
    private readonly string property;

    public FkProps(string property)
    {
        this.property = property;
    }

    public string Value(string prop)
    {
        return this.property;
    }

    public string Value(string prop, string defaults)
    {
        return defaults;
    }

    public IEnumerable<string> Values(string prop)
    {
        return new ListOf<string>(this.property);
    }

    public bool Has(string prop)
    {
        return true;
    }
}