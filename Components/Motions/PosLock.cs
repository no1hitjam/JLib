using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PosLock : VectorLock
{
    private bool _proportional = false;
    private bool _local = true;

    public virtual PosLock Init(Vector3? target = null, float? speed = null, bool? proportional = null, bool? local = null, bool? eased = null)
    {
        _proportional = proportional ?? _proportional;
        _local = local ?? _local;
        base.Init(
            () => this.GetPos(_proportional, _local),
            (v) => this.SetPos(v, _proportional, _local),
            target,
            speed,
            eased);

        return this;
    }


}

