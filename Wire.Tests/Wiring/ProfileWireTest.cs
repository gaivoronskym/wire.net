using Wire.Wiring;

namespace Wire.Tests.Wiring;

public sealed class ProfileWireTest
{
    [Fact]
    public void ProfileMatched()
    {
        Assert.True(
            new ProfileWire("dev")
                .IsActive(new AppContext("profile=dev"), "")
        );
    }

    [Fact]
    public void ProfileArgumentDoesntExist()
    {
        var profile = "test";
        Assert.False(
            new ProfileWire(profile)
                .IsActive(new AppContext($"unknown={profile}"), "")
        );
    }

    [Fact]
    public void ProfileValueDoesntExist()
    {
        Assert.False(
            new ProfileWire("unknown")
                .IsActive(new AppContext("profile=prod"), "")
        );
    }
}