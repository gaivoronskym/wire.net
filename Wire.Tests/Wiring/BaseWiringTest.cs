using System.Reflection;
using System.Xml;
using Wire.Instances;
using Wire.Tests.Fk;
using Wire.Wiring;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Wiring;

public sealed class BaseWiringTest
{
    [Fact]
    public void WiresPlainInstance()
    {
        Assert.True(
            new BaseWiring<bool>(
                new AppContext(Assembly.GetExecutingAssembly()),
                new ListOf<IInstance<bool>>(
                    new Instance<bool>(new FkScalar())
                )
            ).Wire("component").Value()
        );
    }

    [Fact]
    public void WiresInstanceWithWireCondition()
    {
        Assert.True(
            new BaseWiring<bool>(
                new AppContext(Assembly.GetExecutingAssembly(), "--profile=test"),
                new ListOf<IInstance<bool>>(
                    new Instance<bool>(new FkScalar(), new ProfileWire("test1"))
                )
            ).Wire("component2").Value()
        );
    }

    [Fact]
    public void WiresInstanceWithDynamicWireCondition()
    {
        var assembly = Assembly.GetAssembly(this.GetType())!;
        Assert.True(
            new BaseWiring<bool>(
                new AppContext(assembly, "--profile=dev"),
                new ListOf<IInstance<bool>>(
                    new Instance<bool>(new Live<bool>(() => false), new ProfileWire("dev")),
                    new Instance<bool>(new Live<bool>(() => true), new ProfileWire("test"))
                )
            ).With(
                new ListOf<IWire>(
                    new ProfileWire("test")
                )
            ).Wire("component3").Value()
        );
    }

    // [Fact]
    // public void ThrowsExceptionIfWiringConditionsAreNotMet()
    // {
    //     Assert.Throws<IOException>(
    //         () => new BaseWiring<bool>(
    //             new AppContext(Assembly.GetExecutingAssembly()),
    //             new ListOf<IInstance<bool>>(
    //                 new Instance<bool>(new Live<bool>(() => false), new QualifierWire("dev"))
    //             )
    //         ).With(
    //             new ListOf<IWire>(
    //                 new QualifierWire("test")
    //             )
    //         ).Wire("component4").Value()
    //     );
    // }
}