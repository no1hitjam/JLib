using System;
using System.Collections.Generic;
using UnityEngine;

public class Rotation2DLock : FloatLock
{

    /// <param name="target">degrees</param>
    public Rotation2DLock Init(float? target = null, float? speed = null, bool? eased = null)
    {
        base.Init(
            this.GetRotation2D, 
            (a) => { this.Rotate2D(a); }, 
            target, 
            speed, 
            eased);
        return this;
    }

    public override void Update()
    {
        while (_target > this.GetRotation2D() + 180) {
            _target -= 360;
        }
        while (_target < this.GetRotation2D() - 180) {
            _target += 360;
        }
        base.Update();
    }
}
