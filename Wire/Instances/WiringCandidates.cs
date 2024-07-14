using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Wire.Instances;

public sealed class WiringCandidates<T> : ListEnvelope<IInstance<T>>
{
    public WiringCandidates(IEnumerable<IInstance<T>> instances, IAppContext ctx, string component)
        : base(
            new ScalarOf<IList<IInstance<T>>>(
                () => new ListOf<IInstance<T>>(
                    new Filtered<IInstance<T>>(
                        instance => instance.Applicable(ctx, component),
                        instances
                    )
                )
            ),
            false
        )
    {
    }

    public WiringCandidates(IEnumerable<IInstance<T>> instances, IEnumerable<IWire> wires)
        : base(
            new ScalarOf<IList<IInstance<T>>>(
                () => new ListOf<IInstance<T>>(
                    new Filtered<IInstance<T>>(
                        (instance) => instance.Applicable(wires),
                        instances
                    )
                )
            ),
            false
        )
    {
    }
}