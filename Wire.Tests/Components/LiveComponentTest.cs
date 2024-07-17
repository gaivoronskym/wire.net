using Wire.Components;
using Wire.Instances;
using Wire.Wiring;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Components;

public sealed class LiveComponentTest
{
    [Fact]
    public void RetrievesInstance()
    {
        Assert.True(
            new LiveComponent<bool>(
                new AppContext(),
                new Instance<bool>(new True())
            ).Instance()
        );
    }

    [Fact]
    public void RetrievesInstanceWithWireCondition()
    {
        Assert.True(
            new CustomComponent(
                new AppContext("profile=test"), new Integer()
            ).Instance()
        );
    }

    [Fact]
    public void RetrievesInstanceWithSpecifiedWireCondition()
    {
        Assert.True(
            new CustomComponent(
                new AppContext(), new Integer()
            ).With(new ProfileWire("test")).Instance()
        );
    }

    [Fact]
    public void RefreshesInstance()
    {
        var value = new Integer();
        var component = new CustomComponent(
            new AppContext(), value
        );

        component.Instance();
        component.Instance();

        Assert.Equal(
            1,
            value.Value()
        );

        component.Refresh();
        component.Instance();

        Assert.Equal(
            2,
            value.Value()
        );
    }

    [Fact]
    public void RetrievesRefreshedInstance()
    {
        var context = new AppContext("profile=test");
        var component = new CustomComponent(context);

        Assert.True(component.Instance());

        var devComponent = new CustomComponent(context)
            .With(new ProfileWire("dev"));
        devComponent.Refresh();

        Assert.False(
            devComponent.Instance()
        );
    }

    private sealed class CustomComponent(IAppContext ctx, Integer integer) : LiveComponent<bool>
    (
        ctx,
        new Instance<bool>(
            new Live<bool>(() =>
            {
                integer.Increment();
                return false;
            }), new ProfileWire("dev")
        ),
        new Instance<bool>(
            new Live<bool>(() =>
            {
                integer.Increment();
                return true;
            }), new ProfileWire("test")
        )
    )
    {
        public CustomComponent(IAppContext ctx)
            : this(ctx, new Integer())
        {
        }
    }
}