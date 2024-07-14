using Yaapii.Atoms;

namespace Wire.Wiring;

public sealed class EqualClass(InheritanceLevel inheritanceLevel) : IScalar<bool>
{
    public EqualClass(Type derived, Type super)
        : this(new InheritanceLevel(super, derived))
    {
    }

    public bool Value()
    {
        return inheritanceLevel.Value() == 0;
    }
}