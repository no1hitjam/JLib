using System;
using UnityEngine;
using UnityEngine.Events;

public class PosTarget : VectorTargetBase, ICopyable<PosTarget>
{
    private bool _proportional = false;
    private bool _local = true;

    public virtual PosTarget Init(Vector3? target = null, int? time = null, bool? proportional = null, bool? local = null, 
        Vector3? axes = null, EaseType? eased = null, bool? multiplyTimeByDistance = null, UnityEvent invoker = null)
    {
        _proportional = proportional ?? _proportional;
        _local = local ?? _local;
        base.Init(
            () => this.GetPos(_proportional, _local),
            (v) => this.SetPos(v, _proportional, _local),
            target,
            time,
            axes,
            eased,
            multiplyTimeByDistance,
            invoker);

        return this;
    }

    public PosTarget Copy(PosTarget from)
    {
        return Init(from._target, from._maxTime, from._proportional, from._local, from._axes, from._easing, false, null);
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
