using Yaapii.Atoms.List;

namespace Wire.Cli;

public sealed class EmptyArgs() : CommandArgsEnvelope(string.Empty, new ListOf<string>());