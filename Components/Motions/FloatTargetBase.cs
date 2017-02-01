using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FloatTargetBase : Motion
{
    private static readonly Dictionary<EaseType, Func<FloatTargetBase, float>> _floatChange = new Dictionary<EaseType, Func<FloatTargetBase, float>>{
        { EaseType.None, (FloatTargetBase b) => {
            return b.Difference * b.TimeProportion;
        } },
        { EaseType.In, (FloatTargetBase b) => {
            return b.Difference * Mathf.Pow(b.TimeProportion, 2);
        } },
        { EaseType.Out, (FloatTargetBase b) => {
            return -b.Difference * b.TimeProportion * (b.TimeProportion - 2);
        } },
        { EaseType.Both, (FloatTargetBase b) => {
            if (b.TimeProportion < .5f) {
                return b.Difference * Mathf.Pow(b.TimeProportion, 2) * 2;
            }
            return b.Difference *  (Mathf.Pow(b.TimeProportion * 2 - 2, 2) - 2) * -.5f;
        } },
    };

    protected UnityEvent _onEnd;
    public UnityEvent OnEnd { get { return _onEnd; } }

    protected Func<float> _getFloat;
    protected Action<float> _setFloat;

    protected float _start;
    protected float _target;
    protected float _lastChange;
    protected int _time;
    protected int _maxTime = 30;
    private int _originalTime = 30;
    private bool _multiplyTimeByDistance = false;
    protected EaseType _easing = EaseType.Out;
    protected Vector4 _axes = JLib.AxesAll;

    public float TimeProportion { get { return (float)_time / _maxTime; } }
    public float Difference { get { return _target - _start; } }

    public void Awake()
    {
        _onEnd = new UnityEvent();
    }

    /// <param name="axes">default: all</param>
    /// <param name="drift">distance to drift</param>
    protected virtual FloatTargetBase Init(Func<float> getFloat = null, Action<float> setFloat = null,
        float? target = null, int? time = null, EaseType? easing = null, bool? multiplyTimeByDistance = null, UnityEvent invoker = null)
    {
        Init(invoker);

        _getFloat = getFloat ?? _getFloat;
        _start = _getFloat();
        _lastChange = 0;
        _setFloat = setFloat ?? _setFloat;
        _target = target ?? _target;

        _originalTime = time ?? _originalTime;
        _multiplyTimeByDistance = multiplyTimeByDistance ?? _multiplyTimeByDistance;
        if (_multiplyTimeByDistance) {
            _maxTime = (int)(_originalTime * Mathf.Pow(_target - _getFloat(), 2));
        } else {
            _maxTime = _originalTime;
        }
        
        _easing = easing ?? _easing;

        _time = -1;
        if (invoker == null) {
            _time = 0;
        }

        return this;
    }

    public virtual void Update()
    {
        if (ActiveFrame) {
            if (_time >= 0 && _time <= _maxTime) {
                if (_time == _maxTime) {
                    _onEnd.Invoke();
                }
                // WARNING: This method loses some floating point data (is slightly innacurate)
                var newChange = _floatChange[_easing](this);
                _setFloat(_getFloat() + newChange - _lastChange);
                _lastChange = newChange;
                _time++;
            }
        }
    }

    protected override void Wait()
    {
        _time = -1;
    }
}
