using Yaapii.Atoms.List;

namespace Wire.Props;

public sealed class PropertyFileNames(string prefix, IEnumerable<string> suffixes) : ListEnvelope<string>
(
    () =>
        new Joined<string>(
            new ListOf<string>(
                $"{prefix}.properties"
            ),
            new Mapped<string, string>(
                (suffix) => $"{prefix}-{suffix}.properties",
                suffixes
            )
        ),
    false
);