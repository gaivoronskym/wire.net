namespace Wire.Components;

public sealed class Component<T>(IWiring<T> wiring, string name) : IComponent<T>
{

    public Component(IAppContext ctx, IEnumerable<IInstance<T>> instances)
        : this()
    {
    }

    public Component(IWiring<T> wiring)
        : this(wiring, nameof(Component<T>))
    {
    }

    public T Instance()
    {
        throw new NotImplementedException();
    }
}