using Wire.Props;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Wire;

public sealed class AppContext(IEnumerable<IKvp<IProps>> map) : IAppContext
{

    public AppContext()
        : this(new string[]{})
    {
    }

    public AppContext(params string[] args)
        : this(
            new KvpOf<IProps>("app", new AppProps(args)),
            new KvpOf<IProps>("cli", new CliProps(args)),
            new KvpOf<IProps>("qualifiers", new QualifiersProps())
        )
    {
    }

    public AppContext(params IKvp<IProps>[] keys)
        : this(new ListOf<IKvp<IProps>>(keys))
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
        return new Contains<IKvp<IProps>>(
            map,
            (kvp) => kvp.Key().Equals(type)
        );
    }

    private IScalar<IProps> Get(string type)
    {
        return new ScalarOf<IProps>(
            () => new ItemAt<IKvp<IProps>>(
                new Filtered<IKvp<IProps>>(
                    (kvp) => kvp.Key().Equals(type),
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