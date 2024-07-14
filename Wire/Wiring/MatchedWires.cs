using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace Wire.Wiring;

public sealed class MatchedWires(IWire first, IWire second) : IScalar<bool>
{
    
    public bool Value()
    {
        return new EqualClass(first.GetType(), second.GetType()).Value();
    }
}