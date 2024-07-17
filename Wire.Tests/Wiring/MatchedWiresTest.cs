using Wire.Wiring;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wire.Tests.Wiring;

public sealed class MatchedWiresTest
{
    [Fact]
    public void ConditionsMatched()
    {
        var profile = "act";

        Assert.True(
            new MatchedWires(
                new ProfileWire(profile),
                new ProfileWire(profile)
            ).Value()
        );
    }

    [Fact]
    public void ConditionValuesDoesntMatch()
    {
        Assert.False(
            new MatchedWires(
                new ProfileWire("test"),
                new ProfileWire("dev")
            ).Value()
        );
    }

    [Fact]
    public void ConditionsDoesntMatch()
    {
        var txt = "txt";
        Assert.False(
            new MatchedWires(
                new ProfileWire(txt),
                new QualifierWire(txt)
            ).Value()
        );
    }
}