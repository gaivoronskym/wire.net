namespace Wire.Tests;

public sealed class Integer
{
    private int value;

    public Integer()
        : this(0)
    {
    }

    public Integer(int value)
    {
        this.value = value;
    }

    public void Increment()
    {
        value++;
    }

    public int Value()
    {
        return value;
    }
}