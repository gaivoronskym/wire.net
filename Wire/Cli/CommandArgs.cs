namespace Wire.Cli;

public sealed class CommandArgs(string argument, IEnumerable<string> values) : CommandArgsEnvelope(argument, values);