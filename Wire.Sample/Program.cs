using Wire.Props;

namespace Wire.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var props = new QualifiersProps().Has("component");
            Console.ReadKey();
        }
    }
}