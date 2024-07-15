using Wire.Instances;
using Wire.Wiring;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace Wire.Components;

public class Component<T>(IWiring<T> wiring, string name) : IComponent<T>
{

    public Component(IAppContext ctx, params IScalar<T>[] instances)
        : this(ctx, new Mapped<IScalar<T>, IInstance<T>>((input) => new Instance<T>(input), instances))
    {
    }

    public Component(IAppContext ctx, params IInstance<T>[] instances)
        : this(ctx, instances.AsEnumerable())
    {
    }

    public Component(IAppContext ctx, IEnumerable<IInstance<T>> instances)
        : this(new BaseWiring<T>(ctx, instances))
    {
    }

    public Component(IWiring<T> wiring)
        : this(wiring, nameof(Component<T>))
    {
    }

    public Component<T> With(params IWire[] wires)
    {
        return this.With(wires.AsEnumerable());
    }

    public Component<T> With(IEnumerable<IWire> wires)
    {
        return new Component<T>(wiring.With(wires), name);
    }

    public T Instance()
    {
        return wiring.Wire(name).Value();
    }
}