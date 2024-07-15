namespace Wire;

public interface IComponent<T>
{
    IComponent<T> With(IEnumerable<IWire> wires);

    IComponent<T> With(params IWire[] wires);

    T Instance();
}