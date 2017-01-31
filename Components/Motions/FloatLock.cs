using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FloatLock : Motion
{
    protected Func<float> _getFloat;
    protected Action<float> _setFloat;

    protected float _target = 0; 
    protected float _speed = .1f;
    protected bool _eased = true;

    public FloatLock Init(Func<float> getFloat = null, Action<float> setFloat = null, 
        float? target = null, float? speed = null, bool? eased = null)
    {
        _getFloat = getFloat ?? _getFloat;
        _setFloat = setFloat ?? _setFloat;
        _target = target ?? _target;
        _speed = speed ?? _speed;
        _eased = eased ?? _eased;
        
        return this;
    }

    public virtual void Update()
    {
        if (ActiveFrame) {
            _setFloat((_target - _getFloat()) * _speed + _getFloat());
        }

        // TODO: eased set to false
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

