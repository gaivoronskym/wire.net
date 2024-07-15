using Wire.Components;
using Wire.Instances;
using Wire.Wiring;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Fk;

public sealed class FkComponent : Component<IScalar<bool>>
{
    public FkComponent(IAppContext ctx)
        : base(
            ctx,
            new Instance<IScalar<bool>>(
                new ScalarOf<IScalar<bool>>(() => new FkScalar()),
                new QualifierWire(
                    typeof(FkScalar)
                )
            )
        )
    {
    }
}