using System;
using System.Collections.Generic;


public class JRoll<T> : List<T>
{
    private int _offset = 0;

    public int Offset
    {
        get { return _offset; }
        set { _offset = JMath.Mod(value, Count); }
    }

    public new T this[int index]
    {
        get
        {
            return this[JMath.Mod(index + _offset, Count)];
        }

        set
        {
            this[JMath.Mod(index + _offset, Count)] = value;
        }
    }


    public new int IndexOf(T item)
    {
        return JMath.Mod(this.IndexOf(item) - _offset, Count);
    }

    public new void Insert(int index, T item)
    {
        this.Insert(JMath.Mod(index + _offset, Count), item);
    }

    public new void RemoveAt(int index)
    {
        this.RemoveAt(JMath.Mod(index + _offset, Count));
    }
}

