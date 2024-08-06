using Wire.Props;
using Yaapii.Atoms.Scalar;

namespace Wire.Wiring;

public sealed class QualifierWire(string value) : IWire
{

    public QualifierWire(Type cls)
        : this(cls.Name)
    {
    }

    public bool IsActive(IAppContext context, string component)
    {
        var props = new QualifiersPropsOf(context);
        var xpath = $"//class[@name='{component}']";

        return new And(
            new ScalarOf<bool>(() => props.Has(xpath)),
            new ScalarOf<bool>(() => props.Value($"{xpath}/qualifier").Equals(value))
        ).Value();
    }

    public override string ToString()
    {
        return value;
    }
}