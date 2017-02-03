using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputBehaviour : MonoBehaviour
{
    private InputDataEvent _inputEvent;
    private Vector2 _start;
    private bool _down = false;

    private void Awake()
    {
        _inputEvent = new InputDataEvent();
    }

    public InputBehaviour Init(UnityAction<InputData> inputAction)
    {
        _inputEvent.AddListener(inputAction);
        return this;
    }

    void Update()
    {
        var mousePos = new Vector2(
            (Input.mousePosition.x - Screen.width / 2) / 100 * 1.75f, 
            (Input.mousePosition.y - Screen.height / 2) / 100 * 1.75f
        );


        if (Input.GetMouseButtonDown(0)) {
            _start = mousePos;
            _down = true;
            _inputEvent.Invoke(new InputData { Type = InputType.Press, StartPos = _start, Pos = _start });
        } else if (Input.GetMouseButtonUp(0)) {
            _down = false;
            _inputEvent.Invoke(new InputData { Type = InputType.Release, StartPos = _start, Pos = mousePos });
        } else if (_down) {
            _inputEvent.Invoke(new InputData { Type = InputType.Hold, StartPos = _start, Pos = mousePos });
        }
        
    }

}
