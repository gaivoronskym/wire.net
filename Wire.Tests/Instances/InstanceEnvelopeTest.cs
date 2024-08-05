using System.Reflection;
using Wire.Instances;
using Wire.Tests.Fk;
using Wire.Wiring;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Instances;

public sealed class InstanceEnvelopeTest
{
    [Fact]
    public void ChecksIfComponentIsActive()
    {
        Assert.True(
            new CustomInstance()
                .Applicable(new AppContext(Assembly.GetAssembly(typeof(InstanceEnvelopeTest))!,$"profile={Profile()}"), "")
        );
    }

    [Fact]
    public void ChecksIfComponentIsActiveAgainstWires()
    {
        Assert.True(
            new CustomInstance()
                .Applicable(new ListOf<IWire>(Qualifier())
                )
        );
    }

    [Fact]
    public void RefreshesInstance()
    {
        var number = new Integer();
        var instance = new CustomInstance(number);
        instance.Value();
        instance.Value();
        instance.Refresh();
        instance.Value();
        Assert.Equal(
            2,
            number.Value()
        );
    }

    public static string Profile()
    {
        return "test";
    }

    public static IWire Qualifier()
    {
        return new QualifierWire("qualifier");
    }

    public sealed class CustomInstance : InstanceEnvelop<bool>
    {
        public CustomInstance()
            : base(
                () => new Instance<bool>(
                    new ScalarOf<bool>(
                        () => true
                    ),
                    new ProfileWire(Profile()),
                    Qualifier()
                )
            )
        {
        }

        public CustomInstance(Integer value)
            : base(
                new ScalarOf<IInstance<bool>>(
                    () => new Instance<bool>(
                        new Live<bool>(
                            () =>
                            {
                                value.Increment();
                                return true;
                            }
                        ),
                        new FkWire(true)
                    )
                ))
        {
        }
    }
}