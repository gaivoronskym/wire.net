using System.Reflection;
using Wire.Components;
using Wire.Instances;
using Wire.Wiring;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Components;

public sealed class ComponentTest
{
    [Fact]
    public void RetrievesInstance()
    {
        Assert.True(
            new Component<bool>(
                new AppContext(Assembly.GetExecutingAssembly()),
                new True()
            ).Instance()
        );
    }

    [Fact]
    public void RetrievesInstanceWithWireCondition()
    {
        var assembly = Assembly.GetAssembly(this.GetType())!;
        Assert.True(
            new CustomComponent(
                new AppContext(assembly, "profile=test")
            ).Instance()
        );
    }

    [Fact]
    public void RetrievesInstanceWithSpecifiedWireCondition()
    {
        Assert.True(
            new CustomComponent(
                new AppContext(Assembly.GetExecutingAssembly())
            ).With(new ProfileWire("test")).Instance()
        );
    }

    [Fact]
    public void HandlesMultipleInstances()
    {
        var component = new CustomComponent(
            new AppContext(Assembly.GetExecutingAssembly())
        );

        Assert.True(
            component.With(new ProfileWire("test")).Instance()
        );

        Assert.False(
            component.With(new ProfileWire("dev")).Instance()
        );
    }
    

    private sealed class CustomComponent(IAppContext ctx) : Component<bool>
    (
        ctx,
        new Instance<bool>(new False(), new ProfileWire("dev")),
        new Instance<bool>(new True(), new ProfileWire("test"))
    );
}