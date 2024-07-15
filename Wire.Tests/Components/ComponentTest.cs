using Wire.Components;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Components;

public sealed class ComponentTest
{
    [Fact]
    public void RetrievesInstance()
    {
        Assert.True(
            new Component<bool>(
                new AppContext(),
                new True()
            ).Instance()
        );
    }
}