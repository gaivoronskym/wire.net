namespace Wire;

public interface IWiring<T>
{
    IInstance<T> Wire(string component);

    IWiring<T> With(IEnumerable<IWire> wires);
}