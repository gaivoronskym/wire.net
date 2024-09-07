namespace Wire.Sample;

public sealed class RealItems : IItems
{
    public void PrintItem(Guid id)
    {
        Console.WriteLine($"Real item: {id}");
    }
}