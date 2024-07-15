namespace Wire.Cli;

public abstract class CommandArgsEnvelope(string argument, IEnumerable<string> values) : ICommandArgs
{
    public bool Match(string argument)
    {
        return argument.Equals(argument);
    }

    public IEnumerable<string> Values()
    {
        return values;
    }
}