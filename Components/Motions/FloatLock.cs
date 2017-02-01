using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FloatLock : Motion
{
    protected Func<float> _getFloat;
    protected Action<float> _setFloat;

    public float Target = 0; 
    public float Speed = .1f;
    public bool Eased = true;
    public float OverStep = 0;
    private float _lastOverStepMove = 0;

    public FloatLock Init(Func<float> getFloat = null, Action<float> setFloat = null, 
        float? target = null, float? speed = null, bool? eased = null, float? overStep = null)
    {
        _getFloat = getFloat ?? _getFloat;
        _setFloat = setFloat ?? _setFloat;
        Target = target ?? Target;
        Speed = speed ?? Speed;
        Eased = eased ?? Eased;
        OverStep = overStep ?? OverStep;

        return this;
    }


    // TODO: Do Eased
    public virtual void Update()
    {
        if (ActiveFrame && Target != _getFloat()) {
            var move = (Target - _getFloat()) * Speed;
            var overStepMove = move * OverStep + _lastOverStepMove * .9f;
            _lastOverStepMove = overStepMove;
            _setFloat(move + overStepMove + _getFloat());
        }
    }


    protected override void OnInvoked()
    {
        throw new NotImplementedException();
    }

    protected override void Wait()
    {
        throw new NotImplementedException();
    }
}

