using System.Collections.Concurrent;
using Wire.Wiring;
using Yaapii.Atoms.Map;

namespace Wire.Instances;

public sealed class ComponentsContainer : MapEnvelope<string, IEnumerable<IInstance<object>>>
{
    private static IDictionary<string, IEnumerable<IInstance<object>>> map =
        new ConcurrentDictionary<string, IEnumerable<IInstance<object>>>();

    public ComponentsContainer()
        : base(() => map, false)
    {
    }

    public ComponentsContainer(string component, IEnumerable<IInstance<object>> components)
        : base(
            () =>
            {
                new Binary(
                    () => !map.ContainsKey(component),
                    () => map.Add(component, components)
                ).Value();

                return map;
            }
            ,
            false
        )
    {
    }
}