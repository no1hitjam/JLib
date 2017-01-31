using System;
using UnityEngine;

public static class JComponentExtensions
{
    public static T Add<T>(this GameObject gameObject)
        where T : Component
    {
        return gameObject.AddComponent<T>();
    }
    
    public static T Add<T>(this Component component)
        where T : Component
    {
        return component.gameObject.AddComponent<T>();
    }

    public static T Get<T>(this Component component)
        where T : Component
    {
        return component.GetComponent<T>();
    }

    public static T GetOrAdd<T>(this Component component)
        where T : Component
    {
        if (component.Get<T>()) {
            return component.Get<T>();
        }
        return component.Add<T>();
    }

    public static T Copy<T>(this Component component, T copy)
        where T : MonoBehaviour, Copyable<T>
    {
        return component.Add<T>().Copy(copy);
    }

    public static Vector3 GetPos(this Component component, bool proportional = false, bool local = true)
    {
        Vector3 returnPos;
        if (local) {
            returnPos = component.transform.localPosition;
        } else {
            returnPos = component.transform.position;
        }
        // TODO: Proportional

        return returnPos;
    }

    public static void SetPos(this Component component, Vector3 pos, bool proportional = false, bool local = true)
    {
        if (local) {
            component.transform.localPosition = pos;
        } else {
            component.transform.position = pos;
        }
        // TODO: Proportional
    }

    public static Vector3 GetScale(this Component component)
    {
        return component.transform.localScale;
    }

    public static void SetScale(this Component component, Vector3 scale)
    {
        component.transform.localScale = scale;
    }

    public static void SetScale(this Component component, Func<Vector3,Vector3> scale)
    {
        component.transform.localScale = scale(component.transform.localScale);
    }
    
    /// <param name="angle">degrees</param>
    public static void Rotate2D(this Component component, float angle, bool fromIdentity = true)
    {
        if (fromIdentity) {
            component.transform.rotation = Quaternion.identity;
        }
        component.transform.Rotate(Vector3.forward, angle);
    }

    public static float GetRotation2D(this Component component)
    {
        return component.transform.eulerAngles.z;
    }
    
    
}

