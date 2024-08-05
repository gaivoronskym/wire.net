using System.Reflection;
using Wire.Props;
using Yaapii.Atoms.List;

namespace Wire.Tests.Props;

public sealed class AppPropsTest
{
    [Fact]
    public void RetrievesBaseProperty()
    {
        Assert.Equal(
            "localhost",
            AppPropsTest.PropsProd().Value("host")
        );
    }

    [Fact]
    public void RetrievesOverriddenProperty()
    {
        Assert.Equal(
            "9000",
            AppPropsTest.PropsProd().Value("port")
        );
    }

    [Fact]
    public void RetrievesMultipleProperties()
    {
        Assert.Equal(
            2,
            new ListOf<string>(AppPropsTest.PropsTest().Values("formats")).Count
        );
    }

    [Fact]
    public void GetsDefaultProperty()
    {
        var defaults = "default";
        Assert.Equal(
            defaults,
            AppPropsTest.PropsTest().Value("unknown", defaults)
        );
    }

    [Fact]
    public void IgnoresDefaultProperty()
    {
        var defaults = "default";
        Assert.Equal(
            "example.com",
            AppPropsTest.PropsTest().Value("domain", defaults)
        );
    }

    [Fact]
    public void PropertyNotFound()
    {
        Assert.Throws<IOException>(() => AppPropsTest.PropsTest().Value("some props"));
    }

    [Fact]
    public void PropertyFound()
    {
        Assert.True(AppPropsTest.PropsTest().Has("scheme"));
    }

    [Fact]
    public void NoResourcePropertiesFoundOnClasspath()
    {
        Assert.Throws<IOException>(() => new AppProps(Assembly.GetExecutingAssembly()).Value("prop"));
    }

    private static AppProps PropsProd()
    {
        var assembly = Assembly.GetAssembly(typeof(AppPropsTest))!;
        return new AppProps(assembly, "--profile=prod");
    }

    private static AppProps PropsTest()
    {
        var assembly = Assembly.GetAssembly(typeof(AppPropsTest))!;
        return new AppProps(assembly, "--profile=test");
    }
}