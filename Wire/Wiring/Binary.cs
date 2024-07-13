using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace Wire.Wiring;

public sealed class Binary(IScalar<bool> condition, IScalar<bool> scalar) : IScalar<bool>
{
    public Binary(bool condition, Action action)
        : this(new ScalarOf<bool>(condition), action)
    {
    }


    public Binary(IScalar<bool> condition, Action action)
        : this(
            condition,
            new ScalarOf<bool>(
                () =>
                {
                    action();
                    return true;
                }
            )
        )
    {
    }

    public Binary(bool condition, IAction action)
        : this(
            condition,
            new ScalarOf<bool>(
                () =>
                {
                    action.Invoke();
                    return true;
                }
            )
        )
    {
    }

    public Binary(bool condition, IScalar<bool> scalar)
        : this(() => condition, scalar)
    {
    }

    public Binary(Func<bool> condition, IScalar<bool> scalar)
        : this(new ScalarOf<bool>(condition), scalar)
    {
    }

    public bool Value()
    {
        return new Ternary<bool, bool>(
            condition,
            scalar,
            new ScalarOf<bool>(false)
        ).Value();
    }
}