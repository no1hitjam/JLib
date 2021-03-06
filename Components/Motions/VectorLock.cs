﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class VectorLock : Motion
{

    protected Func<Vector4> _getVector;
    protected Action<Vector4> _setVector;

    public Vector4 Target = Vector4.zero;
    private float _distance = 1;
    protected float _speed = .1f;
    protected bool _eased = true;

    public VectorLock Init(Func<Vector4> getVector = null, Action<Vector4> setVector = null,
        Vector4? target = null, float? speed = null, bool? eased = null)
    {
        _getVector = getVector ?? _getVector;
        _setVector = setVector ?? _setVector;
        Target = target ?? Target;
        _speed = speed ?? _speed;
        _eased = eased ?? _eased;
        _distance = Vector4.Distance(Target, _getVector());

        return this;
    }

    public virtual void Update()
    {
        if (ActiveFrame) {
            var speed = _speed;
            if (!_eased && _distance != 0) {
                speed /= _distance;
            }
            _setVector((Target - _getVector()) * speed + _getVector());
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
