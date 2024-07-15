using Yaapii.Atoms.List;

namespace Wire.Props;

public sealed class ProfileNames : ListEnvelope<string>
{
    public ProfileNames(params string[] args)
        : this(new CliProps(args))
    {
    }

    public ProfileNames(IProps props)
        : base(() => new ListOf<string>(props.Values("profile")), false)
    {
    }
}