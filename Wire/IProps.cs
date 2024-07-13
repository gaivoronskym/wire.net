namespace Wire;

public interface IProps
{
    string Value(string prop);

    string Value(string prop, string defaults);

    IEnumerable<string> Values(string prop);

    bool Has(string prop);
}