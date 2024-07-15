using Yaapii.Atoms.Enumerable;

namespace Wire.Cli;

public sealed class CommandLine(IEnumerable<ICommandArgs> args)
{

    public string OptionValue(string name)
    {
        return new ItemAt<string>(
            new ItemAt<ICommandArgs>(
                new Filtered<ICommandArgs>(
                    (arg) => arg.Match(name),
                    args
                ),
                new EmptyArgs()
            ).Value().Values()
        ).Value();
    }

    public IEnumerable<string> OptionValues(string name)
    {
        return new ItemAt<ICommandArgs>(
            new Filtered<ICommandArgs>(
                (arg) => arg.Match(name),
                args
            ),
            new EmptyArgs()
        ).Value().Values();
    }

    public bool HasOption(string name)
    {
        return new Filtered<ICommandArgs>(
            (arg) => arg.Match(name),
            args
        ).Any();
    }
}