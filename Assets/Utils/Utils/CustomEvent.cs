using UnityEngine;
using System;

namespace Alf.Utils
{

public class CustomEvent
{
    private event Action _event;

    public void Invoke()
    {
        _event?.Invoke();
    }

    public void Subscribe(Action listener)
    {
        _event += listener;
    }

    public void Unsubscribe(Action listener)
    {
        _event -= listener;
    }
}

public class CustomEvent<T>
{
    private event Action<T> _event;

    public void Invoke(T t)
    {
        _event?.Invoke(t);
    }

    public void Subscribe(Action<T> listener)
    {
        _event += listener;
    }

    public void Unsubscribe(Action<T> listener)
    {
        _event -= listener;
    }
}

}
