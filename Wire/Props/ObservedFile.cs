using Wire.Wiring;
using Yaapii.Atoms.Scalar;

namespace Wire.Props;

public sealed class ObservedFile
{
    private readonly FileInfo origin;
    private long timestamp;

    public ObservedFile(FileInfo origin)
        : this(origin, origin.LastWriteTime.ToFileTime())
    {
    }

    public ObservedFile(FileInfo origin, long timestamp)
    {
        this.origin = origin;
        this.timestamp = timestamp;
    }

    public bool Modified()
    {
        var modified = origin.LastWriteTime.ToFileTime();

        return new Or(
            new Binary(
                modified == 0L,
                () => throw new IOException($"File {origin.FullName} does not exist.")),
            new Binary(
                modified != timestamp,
                () => this.timestamp = modified
            )
        ).Value();
    }
}