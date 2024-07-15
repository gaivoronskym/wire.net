namespace Wire;

public interface ILiveComponent<T> : IComponent<T>
{
    void Refresh();
}