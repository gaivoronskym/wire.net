namespace Wire.Props;

public sealed class CliProps : IProps
{
    public string Value(string prop)
    {
        throw new NotImplementedException();
    }

    public string Value(string prop, string defaults)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> Values(string prop)
    {
        throw new NotImplementedException();
    }

    public bool Has(string prop)
    {
        throw new NotImplementedException();
    }
}