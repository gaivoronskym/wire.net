namespace Wire.Cli;

public interface ICommandArgs
{
    bool Match(string arg);

    IEnumerable<string> Values();
}