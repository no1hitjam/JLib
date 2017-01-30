using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JDic<TKey, TValue> : Dictionary<TKey, TValue>
{
    private TValue defaultValue;
    private bool hasDefaultValue;

    public JDic()
    {
        hasDefaultValue = false;
    }

    public JDic(TValue defaultValue)
    {
        this.defaultValue = defaultValue;
        hasDefaultValue = true;
    }

    public List<KeyValuePair<TKey, TValue>> ToList()
    {
        var list = new List<KeyValuePair<TKey, TValue>>();
        foreach (var entry in this) {
            list.Add(entry);
        }
        return list;
    }

    public new TValue this[TKey key]
    {
        get
        {
            if (!ContainsKey(key)) {
                if (hasDefaultValue) {
                    return defaultValue;
                } else {
                    Debug.LogError("JDic: key [" + key.ToString() + "] does not exist in " + ToString());
                    Debug.Break();
                    return default(TValue);
                }
            }
            return this[key];
        }

        set
        {
            this[key] = value;
        }
    }
    
}
