using Wire.Props;

namespace Wire.Wiring;

public sealed class ProfileWire(string value) : IWire
{
    public bool IsActive(IAppContext context, string component)
    {
        const string profile = "profile";
        var props = new CliPropsOf(context);
        return props.Has(profile) && props.Value(profile).Equals(value);
    }

    public override string ToString()
    {
        return value;
    }
}