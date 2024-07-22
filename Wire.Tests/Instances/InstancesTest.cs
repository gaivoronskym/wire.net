using Wire.Instances;
using Wire.Tests.Fk;
using Wire.Wiring;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Instances;

public sealed class InstancesTest
{
    [Fact]
    public void RetrievesInstanceValue()
    {
        Assert.True(
            new Instance<bool>(
                new Live<bool>(() => true)
            ).Value()
        );
    }

    [Fact]
    public void InstanceActsAsSingleton()
    {
        var value = new Integer();

        var instance = new Instance<bool>(
            new ScalarOf<bool>(() =>
            {
                value.Increment();
                return true;
            })
        );

        instance.Value();
        instance.Value();

        Assert.Equal(
            1,
            value.Value()
        );
    }

    [Fact]
    public void InstanceIsApplicable()
    {
        Assert.True(
            new Instance<bool>(
                new Live<bool>(() => true),
                new FkWire(true)
            ).Applicable(new AppContext(), "")
        );
    }

    [Fact]
    public void InstanceNotApplicable()
    {
        Assert.False(
            new Instance<bool>(
                new Live<bool>(() => false),
                new FkWire(false)
            ).Applicable(new AppContext(), "")
        );
    }

    [Fact]
    public void InstanceApplicableDependingOnWireConditions()
    {
        Assert.True(
            new Instance<bool>(
                new Live<bool>(() => true),
                new ProfileWire("test")
            ).Applicable(new ListOf<IWire>(new ProfileWire("test")))
        );
    }

    [Fact]
    public void InstanceWithoutWiresIsNotApplicable()
    {
        Assert.False(
            new Instance<bool>(
                new Live<bool>(() => true)
            ).Applicable(new AppContext(), "")
        );
    }

    [Fact]
    public void RefreshesInstance()
    {
        var value = new Integer();
        var instance = new Instance<int>(
            new Live<int>(() =>
            {
                value.Increment();
                return value.Value();
            })
        );

        instance.Value();
        instance.Value();
        instance.Refresh();
        instance.Value();

        Assert.Equal(
            2,
            value.Value()
        );
    }
}