using Wire.Wiring;

namespace Wire.Tests.Wiring;

public sealed class BinaryTest
{
    [Fact]
    public void ConditionTrue()
    {
        var value = false;
        new Binary(
            true,
            () => value = true
        ).Value();
        Assert.True(value);
    }

    [Fact]
    public void ConditionFalse()
    {
        var value = false;
        new Binary(
            false,
            () => value = true
        ).Value();
        Assert.False(value);
    }

    [Fact]
    public void ExecutesScalar()
    {
        Assert.Throws<IOException>(
            () => new Binary(
                true,
                () => throw new IOException("msg")
            ).Value()
        );
    }
}