namespace Wire;

public interface IWire
{
    bool IsActive(IAppContext context, string component);
}