namespace Wire;

public interface IComponent<T>
{
    T Instance();
}