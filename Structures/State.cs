using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State<StateType>
{
    private StateType _state = default(StateType);
    private List<StateType> _nextStates = new List<StateType>();
    private int _time = 0;
    private bool _changing = true;
    private bool _initDone = false;
    private bool _exitDone = true;
    private Dictionary<StateType, StateFunctions> _functions;

    public State(Dictionary<StateType, StateFunctions> functions)
    {
        _nextStates = new List<StateType> { _state };
        _functions = functions;
    }

    public void ChangeState(StateType next_state)
    {
        _nextStates.Add(next_state);
        if (!_changing) {
            _initState();
        }
    }

    private void _endChange()
    {
        _state = _nextStates[0];
        _nextStates = _nextStates.GetRange(1, _nextStates.Count - 1);
        _changing = false;
        _time = 0;
        if (_nextStates.Count > 0) {
            _initState();
        }
    }

    private void _initState()
    {
        _changing = true;
        _exitDone = false;
        _initDone = false;
        _time = 0;
    }

    public void Update()
    {
        if (_functions != null && _functions.ContainsKey(_state)) {
            if (_changing) {
                if (!_exitDone) {
                    _exitDone = _functions[_state].Exit == null || _functions[_state].Exit();
                }
                if (!_initDone) {
                    _initDone = _functions[_state].Init == null || _functions[_state].Init();
                }

                if (_exitDone && _initDone) {
                    _endChange();
                }
            } else if (_functions[_state].Update != null) {
                _functions[_state].Update();
            }
        }
        _time++;
    }

    public void Input(InputData data)
    {
        _functions[_state].Input(data);
    }

    // TODO: Attach this to invoker
    private void TouchInput(InputData data)
    {
        if (_functions[_state].Input != null)
            _functions[_state].Input(data);
    }

    public string StateString()
    {
        if (_changing) {
            return "Changing from " + _state.ToString() + " to " + _nextStates.ToString();
        }
        return _state.ToString();
    }

    public StateType CurrentState
    {
        get { return _state; }
    }

    public StateType NextState
    {
        get { return _nextStates[0]; }
    }

    public bool StateChanging
    {
        get { return _changing; }
    }

    public int StateTime
    {
        get { return _time; }
    }

}

public struct StateFunctions
{
    public Func<bool> Init, Exit;
    public Action Update;
    public UnityAction<InputData> Input;
}
