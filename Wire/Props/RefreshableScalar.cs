using Yaapii.Atoms;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Wire.Props;

public sealed class RefreshableScalar<T> : IScalar<T>
{
    private Solid<T> refreshed;
    private readonly IScalar<T> origin;
    private readonly IAction<T> follow;

    public RefreshableScalar(IScalar<T> origin)
        : this(origin, new ActionOf<T>(_ => {}))
    {
    }

    public RefreshableScalar(IScalar<T> origin, Action action)
        : this(origin, new ActionOf<T>(action))
    {
    }
    
    public RefreshableScalar(IScalar<T> origin, IAction<T> follow)
    {
        this.origin = origin;
        this.refreshed = new Solid<T>(origin);
        this.follow = follow;
    }

    public T Value()
    {
        return this.refreshed.Value();
    }
    
    public void Refresh()
    {
        this.follow.Invoke(this.refreshed.Value());
        this.refreshed = new Solid<T>(this.origin);
    }
}