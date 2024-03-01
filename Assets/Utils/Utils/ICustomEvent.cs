namespace Alf.Utils
{
public interface ICustomEvent
{
    CustomEvent Event { get; }
}

public interface ICustomEvent<T>
{
    CustomEvent<T> Event { get; }
}
}
