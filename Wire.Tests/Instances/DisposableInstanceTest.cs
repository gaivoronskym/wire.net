using Wire.Instances;
using Wire.Tests.Fk;
using Yaapii.Atoms.Scalar;

namespace Wire.Tests.Instances;

public sealed class DisposableInstanceTest
{
    [Fact]
    public void ClosesInstance()
    {
        var number = new Integer();
        new DisposableInstance<IDisposable>(
            new Live<IDisposable>(() => new CustomDisposable(number)),
            new FkWire(true)
        ).Refresh();

        Assert.Equal(
            1,
            number.Value()
        );
    }

    public sealed class CustomDisposable : IDisposable
    {
        private readonly Integer value;

        public CustomDisposable(Integer value)
        {
            this.value = value;
        }

        public void Dispose()
        {
            this.value.Increment();
        }
    }
}