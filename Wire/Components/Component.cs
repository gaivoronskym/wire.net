using Wire.Instances;
using Wire.Wiring;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace Wire.Components;

public sealed class Component<T>(IWiring<T> wiring, string name) : IComponent<T>
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

    public T Instance()
    {
        throw new NotImplementedException();
    }
}