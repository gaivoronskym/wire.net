using Wire.Instances;
using Yaapii.Atoms.List;

namespace Wire.Wiring;

public sealed class WiringBasic<T>(IAppContext ctx, IEnumerable<IInstance<T>> instances, IEnumerable<IWire> wires) : IWiring<T>
{

    public WiringBasic(IAppContext ctx, IEnumerable<IInstance<T>> instances)
        : this(ctx, instances, new ListOf<IWire>())
    {
    }

    public IInstance<T> Wire(string component)
    {
        var cached = new CachedInstances<T>(
            instances, component
        );

        var candidates = new Joined<>()
    }

    public IWiring<T> With(IEnumerable<IWire> wires)
    {
        return new WiringBasic<T>(ctx, instances, wires);
    }
}