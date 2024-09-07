using Wire.Components;
using Wire.Instances;
using Wire.Wiring;
using Yaapii.Atoms.Scalar;

namespace Wire.Sample;

public sealed class ItemsComponent(IAppContext context) : Component<IItems>
(
        context,
        new Instance<IItems>(new Live<IItems>(() => new RealItems()), new ProfileWire("prod")),
        new Instance<IItems>(new Live<IItems>(() => new TestItems()), new ProfileWire("test"))
);