using Yaapii.Atoms;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Text;

namespace Wire;

public sealed class Properties : MapEnvelope<string, string>
{
    public Properties(IInput input)
        : base(
            () =>
            {
                var map = new Dictionary<string, string>();
                var lines = new Split(new TextOf(input), "\r\n");
                foreach (var line in lines)
                {
                    var pair = new Split(
                        line,
                        "="
                    );
                    if (pair.Count() != 2)
                    {
                        continue;
                    }

                    var key = new Trimmed(
                        new TextOf(pair.First)
                    ).AsString();

                    var value = new Trimmed(
                        new TextOf(pair.Last)
                    ).AsString();

                    map.Add(key, value);
                }

                return map;
            },
            false
        )
    {
    }
}