using System;
using UnityEngine;
using UnityEngine.Events;

public class VectorTarget : VectorTargetBase, ICopyable<VectorTarget>
{
    public new VectorTarget Init(Func<Vector4> getVector = null, Action<Vector4> setVector = null, Vector4? target = null,
        int? time = null, Vector4? axes = null, EaseType? easing = null, bool? multiplyTimeByDistance = null, UnityEvent invoker = null)
    {
        base.Init(getVector, setVector, target, time, axes, easing, multiplyTimeByDistance, invoker);
        return this;
    }

    public VectorTarget Copy(VectorTarget from)
    {
        return Init(from._getVector, from._setVector, from._target, from._maxTime, from._axes, from._easing, false, null);
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

