using Wire.Instances;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Instances;

public sealed class CachedInstancesTest
{
    [Fact]
    public void RetrievesCachedComponent()
    {
        var component = "component";
        new CachedInstances<bool>(
            new ListOf<IInstance<bool>>(
                new Instance<bool>(
                    new Live<bool>(() => true)
                )
            ),
            component
        ).GetEnumerator().MoveNext();

        var instances = new CachedInstances<bool>(
            new ListOf<IInstance<bool>>(
                new Instance<bool>(
                    new Live<bool>(() => false)
                )
            ),
            component
        ).GetEnumerator();
        instances.MoveNext();

        Assert.True(
            instances.Current.Value()
        );
    }
}