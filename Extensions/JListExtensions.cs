using System;
using System.Collections.Generic;
using UnityEngine;

public static class JListExtensions
{
    /// <param name="constructor">int is list index</param>
    public static List<T> AddRange<T> (this List<T> list, int count, Func<int,T> constructor)
    {
        for (var i = 0; i < count; i++) {
            list.Add(constructor(list.Count));
        }
        return list;
    }

    /// <summary>
    /// Names componenets according to list index
    /// </summary>
    /// <param name="constructor">constructor for building list entry. int is list index</param>
    public static List<T> AddComponents<T>(this List<T> list, int count, Func<int, T> constructor)
        where T : Component
    {
        for (var i = 0; i < count; i++) {
            list.AddComponent(constructor(list.Count));
            list.Get(-1).GetOrAdd<ID>().SetListIndex(list.Count - 1);
        }
        return list;
    }

    public static void AddComponent<T>(this List<T> list, T component)
        where T : Component
    {
        list.Add(component);
        component.GetOrAdd<ID>().SetListIndex(list.Count - 1);
    }

    /// <summary>
    /// If JRoll, use this to access members without offset
    /// </summary>
    /// <param name="index">rolls if out of bounds</param>
    public static T Get<T> (this List<T> list, int index)
    {
        return list[JMath.Mod(index, list.Count)];
    }

    public static void ForEach<T> (this List<T> list, Action<int,T> action, int start = 0, int end = -1)
    {
        end = JMath.Mod(end, list.Count);
        for (int i = start; i < end; i++) {
            action(i, list.Get(i));
        }
    }

}
