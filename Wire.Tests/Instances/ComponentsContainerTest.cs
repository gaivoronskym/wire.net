using Wire.Instances;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Instances;

public sealed class ComponentsContainerTest
{
    [Fact]
    public void ContainerCachesComponent()
    {
        new ComponentsContainer<bool>(
            "componentName",
            new ListOf<IInstance<bool>>(
                new Instance<bool>(new Live<bool>(() => true))
            )
        ).Count();

        var components = new ComponentsContainer<bool>()["componentName"]
            .GetEnumerator();

        components.MoveNext();

        Assert.True(
            components.Current
                .Value()
        );
    }
}