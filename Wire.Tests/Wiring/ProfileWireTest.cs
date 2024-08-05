using System.Reflection;
using Wire.Tests.Instances;
using Wire.Wiring;

namespace Wire.Tests.Wiring;

public sealed class ProfileWireTest
{
    [Fact]
    public void ProfileMatched()
    {
        var assembly = Assembly.GetAssembly(this.GetType())!;
        Assert.True(
            new ProfileWire("dev")
                .IsActive(new AppContext(assembly, "profile=dev"), "")
        );
    }

    [Fact]
    public void ProfileArgumentDoesntExist()
    {
        var assembly = Assembly.GetAssembly(this.GetType())!;
        var profile = "test";
        Assert.False(
            new ProfileWire(profile)
                .IsActive(new AppContext(assembly, $"unknown={profile}"), "")
        );
    }

    [Fact]
    public void ProfileValueDoesntExist()
    {
        var assembly = Assembly.GetAssembly(this.GetType())!;
        Assert.False(
            new ProfileWire("unknown")
                .IsActive(new AppContext(assembly, "profile=prod"), "")
        );
    }
}