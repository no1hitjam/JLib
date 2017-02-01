using System;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour
{
    private InputDataEvent _inputEvent;
    private Vector2 _start;

    private void Awake()
    {
        _inputEvent = new InputDataEvent();
    }

    public InputBehaviour Init<S>(State<S> state)
    {
        _inputEvent.AddListener(state.Input);
        return this;
    }

    void Update()
    {
        var mouseX = (Input.mousePosition.x - Screen.width / 2) / 100 * 1.75f;
        var mouseY = (Input.mousePosition.y - Screen.height / 2) / 100 * 1.75f;

        if (Input.GetMouseButtonDown(0)) {
            _start = new Vector2(mouseX, mouseY);
        } else if (Input.GetMouseButtonUp(0)) {
            _inputEvent.Invoke(new InputData { Press = _start, Release = new Vector2(mouseX, mouseY) });
        }
        
    }

}
