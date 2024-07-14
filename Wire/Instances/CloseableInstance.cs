using Wire.Props;
using Yaapii.Atoms;

namespace Wire.Instances;

public sealed class CloseableInstance<T>(IScalar<T> scalar, IEnumerable<IWire> wires) : InstanceEnvelop<T>
(
    () => new Instance<T>(
        new RefreshableScalar<T>(
            scalar, scalar.Value().Dispose
        )
    )
) where T : IDisposable
{
    public CloseableInstance(IScalar<T> scalar, params IWire[] wires)
        : this(scalar, wires.AsEnumerable())
    {
    }
}