using Wire.Instances;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Wire.Wiring;

public sealed class BaseWiring<T> : IWiring<T>
{
    private readonly IAppContext ctx;
    private readonly IEnumerable<IInstance<T>> instances;
    private readonly IEnumerable<IWire> wires;

    public BaseWiring(IAppContext ctx, IEnumerable<IInstance<T>> instances)
        : this(ctx, instances, new ListOf<IWire>())
    {
    }

    public BaseWiring(IAppContext ctx, IEnumerable<IInstance<T>> instances, IEnumerable<IWire> wires)
    {
        this.ctx = ctx;
        this.instances = instances;
        this.wires = wires;
    }

    public IInstance<T> Wire(string component)
    {
        var cached = new CachedInstances<T>(
            instances, component
        );

        var candidates = new Joined<IInstance<T>>(
            new WiringCandidates<T>(cached, wires),
            new WiringCandidates<T>(cached, wires)
        );

        return new Ternary<IEnumerable<IInstance<T>>, IInstance<T>>(
            candidates,
            (input) => input.Any(),
            (input) => input.First(),
            (_) => this.instances.First()
        ).Value();
    }

    public IWiring<T> With(IEnumerable<IWire> wires)
    {
        return new BaseWiring<T>(ctx, instances, wires);
    }
}