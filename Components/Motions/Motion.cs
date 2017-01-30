using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Motion : MonoBehaviour
{
    public int FrameDelay = 0;

    protected abstract void OnInvoked();
    protected abstract void Wait();

    protected void Init(UnityEvent invoker = null)
    {
        if (invoker != null) {
            invoker.AddListener(OnInvoked);
            Wait();
        }
    } 

    public bool ActiveFrame
    {
        get
        {
            if (FrameDelay > 0)
                FrameDelay--;
            return FrameDelay <= 0;
        }
    }

    public static T[] InvokeChain<T>(Func<T, UnityEvent> invoker, params T[] motions)
        where T : Motion
    {
        JLib.For(1, motions.Length - 1, (i) =>
        {
            motions[i].Init(invoker(motions[i - 1]));
        });
        motions[0].Init(invoker(motions[motions.Length - 1]));
        motions[0].OnInvoked();
        return motions;
    }
    
}
