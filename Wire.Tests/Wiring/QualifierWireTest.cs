using Wire.Tests.Fk;
using Wire.Wiring;

namespace Wire.Tests.Wiring;

public sealed class QualifierWireTest
{
    [Fact]
    public void MatchesQualifiers()
    {
        Assert.True(
            new QualifierWire(nameof(FkScalar)).IsActive(new AppContext(), nameof(FkComponent))
        );
    }

    [Fact]
    public void QualifierNotMatched()
    {
        Assert.False(
            new QualifierWire("sth").IsActive(new AppContext(), "")
        );
    }

    [Fact]
    public void QualifierAsString()
    {
        var qualifier = "qualifier";
        Assert.Equal(
            qualifier,
            new QualifierWire(qualifier).ToString()
        );
    }
}