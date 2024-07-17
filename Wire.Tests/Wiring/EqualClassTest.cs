using Wire.Wiring;

namespace Wire.Tests.Wiring;

public sealed class EqualClassTest
{
    [Fact]
    public void ClassesAreEqual()
    {
        Assert.True(
            new EqualClass(typeof(Integer), typeof(Integer)).Value()
        );
    }

    [Fact]
    public void ClassesAreDifferent()
    {
        Assert.False(
            new EqualClass(typeof(Integer), typeof(EqualClass)).Value()
        );
    }
}