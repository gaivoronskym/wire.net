using Wire.Props;
using Yaapii.Atoms;

namespace Wire.Instances;

public sealed class DisposableInstance<T>(IScalar<T> scalar, IEnumerable<IWire> wires) : InstanceEnvelop<T>
(
    () => new Instance<T>(
        new RefreshableScalar<T>(
            scalar, scalar.Value().Dispose
        ),
        wires
    )
) where T : IDisposable
{
    public DisposableInstance(IScalar<T> scalar, params IWire[] wires)
        : this(scalar, wires.AsEnumerable())
    {
    }
}