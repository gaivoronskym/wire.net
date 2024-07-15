using Wire.Cli;
using Yaapii.Atoms;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Wire.Props;

public sealed class CliProps : IProps
{
    private static IFunc<IEnumerable<string>, CommandLine> Singleton =
        new StickyFunc<IEnumerable<string>, CommandLine>(
            (arguments) =>
            {
                var map = new Dictionary<string, IList<string>>();

                foreach (var argument in arguments)
                {
                    var pair = new Split(argument, "=");
                    if (pair.Count() != 2)
                    {
                        continue;
                    }

                    var key = pair.First();
                    var value = pair.Last();

                    if (!map.ContainsKey(key))
                    {
                        map.Add(key, new List<string>());
                    }

                    map[key].Add(value);
                }
                var commandArgs = new List<ICommandArgs>();

                foreach (var (key, value) in map)
                {
                    commandArgs.Add(new CommandArgs(key, value));
                }

                return new CommandLine(commandArgs);
            }
        );

    private readonly IEnumerable<string> args;

    public CliProps(params string[] args)
        : this(args.AsEnumerable())
    {
    }

    public CliProps(IEnumerable<string> args)
    {
        this.args = args;
    }

    public string Value(string prop)
    {
        return CliProps.Singleton.Invoke(this.args).OptionValue(prop);
    }

    public string Value(string prop, string defaults)
    {
        return new Ternary<bool, string>(
            new ScalarOf<bool>(
                () => this.Has(prop)
            ),
            new ScalarOf<string>(
                () => this.Value(prop)
            ),
            new ScalarOf<string>(() => defaults)
        ).Value();
    }

    public IEnumerable<string> Values(string prop)
    {
        return new Ternary<bool, IEnumerable<string>>(
            new ScalarOf<bool>(
                () => this.Has(prop)
            ),
            new ScalarOf<IEnumerable<string>>(
                () => new ListOf<string>(
                    CliProps.Singleton.Invoke(this.args).OptionValues(prop)
                )
            ),
            new ScalarOf<IEnumerable<string>>(
                () => new ListOf<string>()
            )
        ).Value();
    }

    public bool Has(string prop)
    {
        return new FuncWithFallback<string, bool>(
            input => CliProps.Singleton.Invoke(this.args).HasOption(input),
            (_) => false
        ).Invoke(prop);
    }
}