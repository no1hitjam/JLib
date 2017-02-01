using System;
using System.Collections.Generic;
using UnityEngine;

public class Rotation2DLock : FloatLock
{
    float wobble = 0f;

    /// <param name="target">degrees</param>
    public Rotation2DLock Init(float? target = null, float? speed = null, bool? eased = null, float? overStep = null)
    {
        base.Init(
            this.GetRotation2D, 
            (a) => { this.Rotate2D(a); }, 
            target, 
            speed, 
            eased,
            overStep);
        return this;
    }

    public override void Update()
    {
        while (this.GetRotation2D() + 180 < Target) {
            Target -= 360;
        }
        while (this.GetRotation2D() - 180 > Target) {
            Target += 360;
        }
        base.Update();
    }
}
