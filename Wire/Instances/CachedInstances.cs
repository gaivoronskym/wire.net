using Yaapii.Atoms.List;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Wire.Instances;

public sealed class CachedInstances<T> : ListEnvelope<IInstance<T>>
{
    public CachedInstances(IEnumerable<IInstance<T>> instances, string component)
        : this(
            component,
            new MapOf<IEnumerable<IInstance<T>>>(
                new KeyValuePair<string, IEnumerable<IInstance<T>>>(component, instances)
            )
        )
    {
    }

    public CachedInstances(string component, IDictionary<string, IEnumerable<IInstance<T>>> map)
        : base(
            new ScalarOf<IList<IInstance<T>>>(
                () => new ListOf<IInstance<T>>(
                    map.FirstOrDefault(x => x.Key == component, new KeyValuePair<string, IEnumerable<IInstance<T>>>()).Value
                )
            ),
            false
        )
    {
    }
}