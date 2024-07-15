namespace Wire.Tests.Fk;

public sealed class FkWire(bool condition) : IWire
{
    public bool IsActive(IAppContext context, string component)
    {
        return condition;
    }
}