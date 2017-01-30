using System;
using System.Collections.Generic;


public class JQueuePair<T>
{
    private T _current;
    private T _last;

    private Action<T> Do;
    private Action<T> Undo;

    public JQueuePair(T first, Action<T> Do, Action<T> Undo)
    {
        this.Do = Do;
        this.Undo = Undo;
        _current = first;
        _last = first;
    }

    public void Add(T item)
    {
        if (item.Equals(_current)) {
            return;
        }
        Undo(_last);
        Do(item);

        _last = _current;
        _current = item;
    }
}

