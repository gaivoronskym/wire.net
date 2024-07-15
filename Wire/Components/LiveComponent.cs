using Wire.Instances;
using Wire.Wiring;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace Wire.Components;

public class LiveComponent<T>(IWiring<T> wiring) : IComponent<T>
{
    public LiveComponent(IAppContext ctx, params IScalar<T>[] instances)
        : this(ctx, new Mapped<IScalar<T>, IInstance<T>>(input => new Instance<T>(input), instances))
    {
    }

    public LiveComponent(IAppContext ctx, params IInstance<T>[] instances)
        : this(ctx, instances.AsEnumerable())
    {
    }

    public LiveComponent(IAppContext ctx, IEnumerable<IInstance<T>> instances)
        : this(new BaseWiring<T>(ctx, instances))
    {
    }

    public LiveComponent<T> With(params IWire[] wires)
    {
        return this.With(wires.AsEnumerable());
    }

    public LiveComponent<T> With(IEnumerable<IWire> wires)
    {
        return new LiveComponent<T>(wiring.With(wires));
    }

    public T Instance()
    {
        return wiring.Wire(nameof(LiveComponent<T>)).Value();
    }

    public void Refresh()
    {
        this.RefreshInstance();
    }

    private IInstance<T> RefreshInstance()
    {
        var instance = wiring.Wire(
            nameof(LiveComponent<T>)
        );

        instance.Refresh();
        return instance;
    }
}