using System;
using UnityEngine;
using UnityEngine.Events;

public class VectorTarget : VectorTargetBase, Copyable<VectorTarget>
{
    public new VectorTarget Init(Func<Vector4> getVector = null, Action<Vector4> setVector = null, Vector4? target = null,
        int? time = null, Vector4? axes = null, EaseType? easing = null, UnityEvent invoker = null)
    {
        base.Init(getVector, setVector, target, time, axes, easing, invoker);
        return this;
    }

    public VectorTarget Copy(VectorTarget from)
    {
        return Init(from._getVector, from._setVector, from._target, from._maxTime, from._axes, from._easing, null);
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void OnInvoked()
    {
        Init();
    }
}

