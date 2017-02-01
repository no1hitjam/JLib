using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum Axis { X, Y, Z, W };

public static class JLib
{
    // -- enums --
    public enum TextFitMode { StretchX, ShrinkX, StretchY, ShrinkY }
    

    // -- Constants --
    public readonly static Vector4 AxesAll = Vector4.one;
    public readonly static Vector4 Axes2D = new Vector4(1, 1, 0, 0);

    // -- Axes --

    public static Vector4 Axes(bool x, bool y, bool z, bool w = false)
    {
        return new Vector4(x ? 1 : 0, y ? 1 : 0, z ? 1 : 0, w ? 1 : 0);
    }

    /// <summary>
    /// Rounds each float to 0 or 1 from 0 or not-zero.
    /// Sets null to all
    /// </summary>
    public static Vector4 ReduceToAxes(Vector4? v)
    {
        if (!v.HasValue)
            return AxesAll;
        return new Vector4(v.Value.x < .0001 ? 0 : 1, v.Value.y < .0001 ? 0 : 1, v.Value.z < .0001 ? 0 : 1, v.Value.w < .0001 ? 0 : 1);
    }

    public static Vector4 GetAxes(Axis axis)
    {
        switch (axis) {
            case Axis.Y: return new Vector4(0, 1, 0, 0);
            case Axis.Z: return new Vector4(0, 0, 1, 0);
            case Axis.W: return new Vector4(0, 0, 0, 1);
        }
        return new Vector4(1, 0, 0, 0);
    }

    // -- JBehaviour --

    public static T New<T>(string name = "New GameObject") 
        where T : Component
    {
        var component = new GameObject().AddComponent<T>();
        component.Add<ID>().Init(name);
        return component;
    }

    // -- iteration --
    /*public static void For(int iterations, UnityAction<int> action)
    {
        For(0, iterations, action);
    }

    /// <param name="step">defaults to increment</param>
    public static void For(int start, int iterations, UnityAction<int> action)
    {
        for (int i = start; i < start + iterations; i++) {
            action(i);
        }
    }*/

    public static void ForEach<T>(Action<int, T> action, params T[][] arrays)
    {
        foreach (var array in arrays) {
            for (int i = 0; i < array.Length; i++) {
                action(i, array[i]);
            }
        }
    }

    public static void ForEach<T>(Action<int, T> action, params List<T>[] lists)
    {
        foreach (var list in lists) {
            for (int i = 0; i < list.Count; i++) {
                action(i, list[i]);
            }
        }
    }
}


public interface ICopyable<T>
{
    T Copy(T from);
}

public class Pair<T, U>
{
    public Pair()
    {
    }

    public Pair(T first, U second)
    {
        First = first;
        Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
};