namespace Wire;

public interface IInstance<T>
{
    bool Applicable(IAppContext context, string component);

    bool Applicable(IEnumerable<IWire> wires);

    T Value();

    void Refresh();
}