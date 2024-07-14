using System.Reflection;
using Yaapii.Atoms;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;

namespace Wire.Props;

public sealed class InputsFromFileNames : ListEnvelope<IInput>
{
    public InputsFromFileNames(IEnumerable<string> names)
        : base(
            () => new Mapped<string, IInput>(
                (input) => new ResourceOf(input, Assembly.GetAssembly(typeof(InputsFromFileNames))),
                names
            ),
            false
        )
    {
    }

    public InputsFromFileNames(string path, IEnumerable<string> names)
        : base(
            () => new Mapped<string, IInput>(
                (input) => new InputOf(new Uri(input)),
                new Mapped<string, string>(
                    (input) => $"{path}/{input}",
                    names
                )
            ),
            false
        )
    {
    }
}