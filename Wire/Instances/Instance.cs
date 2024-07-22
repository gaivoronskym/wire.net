using Wire.Props;
using Wire.Wiring;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace Wire.Instances;

public sealed class Instance<T> : IInstance<T>
{
    private readonly RefreshableScalar<T> scalar;
    private readonly IEnumerable<IWire> wires;

    public Instance(IScalar<T> instance, params IWire[] wires)
        : this(instance, wires.AsEnumerable())
    {
    }
    
    public Instance(IScalar<T> instance, IEnumerable<IWire> wires)
        : this(new RefreshableScalar<T>(instance), wires)
    {
    }
    
    public Instance(RefreshableScalar<T> instance, IEnumerable<IWire> wires)
    {
        this.scalar = instance;
        this.wires = wires;
    }

    public bool Applicable(IAppContext context, string component)
    {
        return new Or<IWire>(
            (input) => input.IsActive(
                context, component
            ),
            this.wires
        ).Value();
    }

    public bool Applicable(IEnumerable<IWire> ext)
    {
        return new Or<IWire>(
            (input) => new Or<IWire>(
                (wire) => new MatchedWires(
                    input, wire
                ).Value(),
                ext
            ).Value(),
            this.wires
        ).Value();
    }

    public T Value()
    {
        return this.scalar.Value();
    }

    public void Refresh()
    {
        this.scalar.Refresh();
    }
}