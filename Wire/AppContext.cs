using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Wire;

public sealed class AppContext(IEnumerable<IKvp<string, IProps>> map) : IAppContext
{
    public AppContext(params IKvp<string, IProps>[] keys)
        : this(new ListOf<IKvp<string, IProps>>(keys))
    {
    }

    public IProps Props(string type)
    {
        return new Ternary<bool, IProps>(
            Has(type),
            Get(type),
            Fail(type)
        ).Value();
    }

    private IScalar<bool> Has(string type)
    {
        return new Contains<IKvp<string, IProps>>(
            map,
            (kvp) => kvp.Value().Equals(type)
        );
    }

    private IScalar<IProps> Get(string type)
    {
        return new ScalarOf<IProps>(
            () => new ItemAt<IKvp<string, IProps>>(
                new Filtered<IKvp<string, IProps>>(
                    (kvp) => kvp.Value().Equals(type),
                    map
                )
            ).Value().Value()
        );
    }

    private IScalar<IProps> Fail(string type)
    {
        return new ScalarOf<IProps>(
            () => throw new IOException($"No properties found for namespace {type}")
        );
    }
}