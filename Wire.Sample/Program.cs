using Wire.Props;
using Yaapii.Atoms.Map;

namespace Wire.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var component = new ItemsComponent(
                new AppContext(
                    new KvpOf<IProps>("cli", new CliProps("--profile=test"))
                )
            );

            component.Instance().PrintItem(Guid.NewGuid());
            Console.ReadKey();
        }
    }
}