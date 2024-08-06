using System.Reflection;
using Wire.Props;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Wire.Tests.Props;

public class BasicPropsTest
{
    [Fact]
    public void GetsPropertyValue()
    {
        var property = "connectionString";
        var value = "http://localhost";
        Assert.Equal(
            value,
            BasicPropsTest.CfgProps(property, value).Value(property)
        );
    }

    [Fact]
    public void GetsPropertyValues()
    {
        var property = "connectionString";
        var value = "localhost,domain";
        Assert.Equal(
            2,
            new ListOf<string>(
                BasicPropsTest.CfgProps(property, value).Values(property)
            ).Count
        );
    }

    [Fact]
    public void ReturnsDefaultPropertyValue()
    {
        var property = "unknown";
        var value = "value";
        var defaults = "defaults";

        Assert.Equal(
            defaults,
            BasicPropsTest.CfgProps(property, value).Value("prop", defaults)
        );
    }

    [Fact]
    public void ReadPropertyFromFile()
    {
        Assert.Equal(
            "localhost",
            new BasicProps(
                new ResourceOf(
                    "app.properties",
                    Assembly.GetExecutingAssembly()
                )
            ).Value("host", "default")
        );
    }

    [Fact]
    public void PropertyNotFound()
    {
        Assert.Throws<IOException>(
            () => BasicPropsTest.CfgProps("some property", "some value").Value("prop")
        );
    }

    private static BasicProps CfgProps(string property, string value)
    {
        return new BasicProps(
            new InputOf(
                new TextOf($"{property}={value}")
            )
        );
    }
}