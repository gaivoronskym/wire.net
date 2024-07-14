using Yaapii.Atoms;
using Yaapii.Atoms.List;

namespace Wire;

public sealed class AppContext(IEnumerable<IKvp<string, IProps>> map) : IAppContext
{
    public AppContext(params IKvp<string, IProps>[] keys)
        : this(new ListOf<IKvp<string, IProps>>(keys))
    {
    }

    public IProps Props(string type)
    {
        throw new NotImplementedException();
    }
}