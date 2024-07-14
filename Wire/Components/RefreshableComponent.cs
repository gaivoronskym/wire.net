using Wire.Instances;
using Wire.Wiring;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace Wire.Components;

public sealed class RefreshableComponent<T>(IWiring<T> wiring) : IComponent<T>
{

    public RefreshableComponent(IAppContext ctx, params IScalar<T>[] instances)
        : this(ctx, new Mapped<IScalar<T>, IInstance<T>>(input => new Instance<T>(input), instances))
    {
    }

    public RefreshableComponent(IAppContext ctx, params IInstance<T>[] instances)
        : this(ctx, instances.AsEnumerable())
    {
    }

    public RefreshableComponent(IAppContext ctx, IEnumerable<IInstance<T>> instances)
        : this(new BaseWiring<T>(ctx, instances))
    {
    }

    public RefreshableComponent<T> With(params IWire[] wires)
    {
        return this.With(wires.AsEnumerable());
    }

    public RefreshableComponent<T> With(IEnumerable<IWire> wires)
    {
        return new RefreshableComponent<T>(wiring.With(wires));
    }

    public T Instance()
    {
        return wiring.Wire(nameof(RefreshableComponent<T>)).Value();
    }

    public void Refresh()
    {
        this.RefreshInstance();
    }

    private IInstance<T> RefreshInstance()
    {
        var instance = wiring.Wire(
            nameof(RefreshableComponent<T>)
        );

        instance.Refresh();
        return instance;
    }
}