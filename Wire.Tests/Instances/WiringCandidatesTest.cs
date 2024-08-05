using System.Reflection;
using Wire.Instances;
using Wire.Wiring;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Instances;

public sealed class WiringCandidatesTest
{
    [Fact]
    public void ChoosesBothCandidatesForWiring()
    {
        var assembly = Assembly.GetAssembly(typeof(WiringCandidatesTest))!;
        var profile = "test";
        var context = new AppContext(
            assembly,
            $"profile={profile}"
        );

        var candidates = new WiringCandidates<bool>(
            new ListOf<IInstance<bool>>(
                new Instance<bool>(new Live<bool>(() => false)),
                new Instance<bool>(new Live<bool>(() => true), new ProfileWire(profile))
            ),
            context,
            ""
        ).GetEnumerator();

        candidates.MoveNext();

        Assert.True(candidates.Current.Value());
    }

    [Fact]
    public void ChoosesCandidatesWithAdditionalWiringOptions()
    {
        var wire = new ProfileWire("dev");

        var candidates = new WiringCandidates<bool>(
            new ListOf<IInstance<bool>>(
                new Instance<bool>(new Live<bool>(() => false)),
                new Instance<bool>(new Live<bool>(() => true), wire)
            ),
            new ListOf<IWire>(wire)
        ).GetEnumerator();

        candidates.MoveNext();
        Assert.True(candidates.Current.Value());
    }
}