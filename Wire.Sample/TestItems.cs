namespace Wire.Sample;

public sealed class TestItems : IItems
{
    public void PrintItem(Guid id)
    {
        Console.WriteLine($"Test item: {id}");
    }
}