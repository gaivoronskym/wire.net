using System.Collections.Concurrent;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Wire.Instances;

public sealed class ComponentsContainer : MapEnvelope<string, IEnumerable<IInstance<object>>>
{
    private static IDictionary<string, IEnumerable<IInstance<object>>> map =
        new ConcurrentDictionary<string, IEnumerable<IInstance<object>>>();

    public ComponentsContainer(string component, IEnumerable<IInstance<object>> components)
        : base(
            new Func<IDictionary<string, IEnumerable<IInstance<object>>>>(
                () =>
                {
                    new 
                }
            )
        )
    {
    }
}