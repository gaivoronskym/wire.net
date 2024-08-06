using Wire.Props;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Wire.Tests.Props;

public sealed class XmlPropsTest
{
    [Fact]
    public void GetsPropertyValue()
    {
        var property = "prop";
        var value = "value";
        Assert.Equal(
            value,
            XmlPropsTest.Props(property, value)
                .Value(property)
        );
    }

    [Fact]
    public void GetsPropertyValues()
    {
        var property = "multi";
        var value = "value";

        Assert.Equal(
            1,
            new ListOf<string>(
                XmlPropsTest.Props(property, value)
                    .Values(property)
            ).Count
        );
    }

    [Fact]
    public void GetsDefaultProperty()
    {
        var property = "unknown";
        var value = "no";
        var defaults = "defaults";

        Assert.Equal(
            defaults,
            XmlPropsTest.Props(property, value)
                .Value("", defaults)
        );
    }

    [Fact]
    public void GetsPropertyFromFile()
    {
        Assert.Equal(
            "FkScalar",
            new XmlProps(
                new InputOf(
                    new Uri(
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "qualifiers.xml")
                    )
                )
            ).Value("//class/qualifier", "def")
        );
    }

    private static XmlProps Props(string property, string value)
    {
        return new XmlProps(
            new InputOf(
                new TextOf(
                    $"<{property}>{value}</{property}>"
                )
            )
        );
    }

    private static string Prop(string property)
    {
        return $"//{property}";
    }
}