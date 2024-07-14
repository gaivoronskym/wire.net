using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace Wire.Props;

public abstract class PropsEnvelope(IScalar<IProps> origin) : IProps
{
    public PropsEnvelope(IProps props)
        : this(() => props)
    {
    }

    public PropsEnvelope(Func<IProps> func)
        : this(new ScalarOf<IProps>(func))
    {
    }

    public string Value(string prop)
    {
        return origin.Value().Value(prop);
    }

    public string Value(string prop, string defaults)
    {
        return origin.Value().Value(prop, defaults);
    }

    public IEnumerable<string> Values(string prop)
    {
        return origin.Value().Values(prop);
    }

    public bool Has(string prop)
    {
        return origin.Value().Has(prop);
    }
}