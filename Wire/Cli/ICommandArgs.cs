namespace Wire.Cli;

public interface ICommandArgs
{
    bool Match(string argument);

    IEnumerable<string> Values();
}