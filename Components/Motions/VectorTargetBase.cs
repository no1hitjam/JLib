using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EaseType { In, Out, Both, None }

public abstract class VectorTargetBase : Motion
{
    private static readonly Dictionary<EaseType, Func<VectorTargetBase, Vector4>> _vectorChange = new Dictionary<EaseType, Func<VectorTargetBase, Vector4>>{
        { EaseType.None, (VectorTargetBase b) => {
            return b.Difference * b.TimeProportion;
        } },
        { EaseType.In, (VectorTargetBase b) => {
            return b.Difference * Mathf.Pow(b.TimeProportion, 2);
        } },
        { EaseType.Out, (VectorTargetBase b) => {
            return -b.Difference * b.TimeProportion * (b.TimeProportion - 2);
        } },
        { EaseType.Both, (VectorTargetBase b) => {
            if (b.TimeProportion < .5f) {
                return b.Difference * Mathf.Pow(b.TimeProportion, 2) * 2;
            }
            return b.Difference *  (Mathf.Pow(b.TimeProportion * 2 - 2, 2) - 2) * -.5f;
        } },
    };

    protected UnityEvent _onEnd;
    public UnityEvent OnEnd { get { return _onEnd; } }

    protected Func<Vector4> _getVector;
    protected Action<Vector4> _setVector;

    protected Vector4 _start;
    protected Vector4 _target = Vector4.zero;
    protected Vector4 _lastChange;
    protected int _time;
    protected int _maxTime = 30;
    protected EaseType _easing = EaseType.Out;
    protected Vector4 _axes = JLib.AxesAll;
    
    public float TimeProportion { get { return (float)_time / _maxTime; } }
    public Vector4 Difference { get { return _target.NewScale(_axes) - _start.NewScale(_axes); } }

    public void Awake()
    {
        _onEnd = new UnityEvent();
    }

    /// <param name="axes">default: all</param>
    /// <param name="drift">distance to drift</param>
    protected virtual VectorTargetBase Init(Func<Vector4> getVector = null, Action<Vector4> setVector = null, Vector4? target = null, 
        int? time = null, Vector4? axes = null, EaseType? easing = null, UnityEvent invoker = null)
    {
        Init(invoker);

        _getVector = getVector ?? _getVector;
        _start = _getVector();
        _lastChange = Vector4.zero;
        _setVector = setVector ?? _setVector;
        _target = target ?? _target;
        _maxTime = time ?? _maxTime;
        _axes = axes ?? _axes;
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
                var newChange = _vectorChange[_easing](this);
                _setVector(_getVector() + newChange - _lastChange);
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
