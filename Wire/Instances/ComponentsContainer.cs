using System.Collections.Concurrent;
using Wire.Wiring;
using Yaapii.Atoms.Map;

namespace Wire.Instances;

public sealed class ComponentsContainer<T> : MapEnvelope<string, IEnumerable<IInstance<T>>>
{
    private static IDictionary<string, IEnumerable<IInstance<T>>> map =
        new ConcurrentDictionary<string, IEnumerable<IInstance<T>>>();

    public ComponentsContainer()
        : base(() => map, false)
    {
    }

    public ComponentsContainer(string component, IEnumerable<IInstance<T>> components)
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