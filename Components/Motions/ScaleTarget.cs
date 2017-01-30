using UnityEngine;
using UnityEngine.Events;

public class ScaleTarget : VectorTargetBase, Copyable<ScaleTarget>
{
    public virtual ScaleTarget Init(Vector3 target, int? time = null, Vector3? axes = null, EaseType? easing = null,
        UnityEvent invoker = null)
    {
        base.Init(
            () => { return this.GetScale(); },
            (v) => { this.SetScale(v); }, 
            target, time, axes, easing, invoker);
        return this;
    }

    public ScaleTarget Copy(ScaleTarget from)
    {
        return Init(from._target, from._maxTime, from._axes, from._easing, null);
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

