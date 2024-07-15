using Yaapii.Atoms;

namespace Wire.Tests.Fk;

public sealed class FkScalar : IScalar<bool>
{
    public bool Value()
    {
        return true;
    }
}