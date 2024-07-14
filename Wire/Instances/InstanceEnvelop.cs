using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace Wire.Instances;

public abstract class InstanceEnvelop<T>(IScalar<IInstance<T>> origin) : IInstance<T>
{
    protected InstanceEnvelop(Func<IInstance<T>> func)
        : this(new ScalarOf<IInstance<T>>(func))
    {
    }

    public bool Applicable(IAppContext context, string component)
    {
        return origin.Value().Applicable(context, component);
    }

    public bool Applicable(IEnumerable<IWire> wires)
    {
        return origin.Value().Applicable(wires);
    }

    public T Value()
    {
        return origin.Value().Value();
    }

    public void Refresh()
    {
        origin.Value().Refresh();
    }
}