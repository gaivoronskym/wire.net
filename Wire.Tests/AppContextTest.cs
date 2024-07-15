namespace Wire.Tests;

public sealed class AppContextTest
{
    [Fact]
    public void ContextContainsOptions()
    {
        Assert.NotNull(
            new AppContext()
                .Props("cli")
        );
    }

    [Fact]
    public void ContextContainsConfig()
    {
        Assert.NotNull(
            new AppContext().Props("app")
        );
    }

    [Fact]
    public void ContextContainsDependencies()
    {
        Assert.NotNull(
            new AppContext().Props("qualifiers")
        );
    }
}