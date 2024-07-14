using Yaapii.Atoms;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Wire.Props;

public sealed class RefreshableScalar<T> : IScalar<T>
{
    private IScalar<T> refreshed;
    private readonly IScalar<T> origin;
    private readonly IAction<T> action;

    public RefreshableScalar(IScalar<T> origin)
        : this(origin, new ActionOf<T>(input => {}))
    {
    }

    public RefreshableScalar(IScalar<T> origin, Action action)
        : this(origin, new ActionOf<T>(action))
    {
    }
    
    public RefreshableScalar(IScalar<T> origin, IAction<T> action)
    {
        this.refreshed = new ScalarOf<T>(origin);
        this.origin = origin;
        this.action = action;
    }

    public T Value()
    {
        return this.refreshed.Value();
    }
    
    public void Refresh()
    {
        this.action.Invoke(this.refreshed.Value());
        this.refreshed = new ScalarOf<T>(origin);
    }
}