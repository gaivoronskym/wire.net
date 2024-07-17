namespace Wire.Cli;

public abstract class CommandArgsEnvelope : ICommandArgs
{
    private readonly string argument;
    private readonly IEnumerable<string> values;

    protected CommandArgsEnvelope(string argument, IEnumerable<string> values)
    {
        this.argument = argument;
        this.values = values;
    }

    public bool Match(string arg)
    {
        return this.argument.Equals(arg);
    }

    public IEnumerable<string> Values()
    {
        return values;
    }
}