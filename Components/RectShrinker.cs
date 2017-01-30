using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class RectShrinker : MonoBehaviour
{
    public float MaxSize;
    public Vector3 Axes;
    private JQueuePair<float> _changeSize;
    private float _lastSize = 0;

    public RectShrinker Init(float maxSize, Vector3 axes)
    {
        MaxSize = maxSize;
        Axes = axes;
        return this;
    }

    void Start()
    {
        // multiply by changed size proportion and divide by old
        _changeSize = new JQueuePair<float>(1,
            (mult) => this.SetScale((v) => v.NewScale(Axes * mult)),
            (mult) => this.SetScale((v) => v.NewScale(Axes / mult)));
    }

    void Update()
    {
        // when size changes, check if bigger than max size
        // if so, scale to proportion
        var size = this.Get<RectTransform>().sizeDelta.Magnitude(Axes);
        if (size > MaxSize && size != _lastSize) {
            _changeSize.Add(MaxSize / size);
            _lastSize = size;
        } else {
            _changeSize.Add(1);
        }
    }
    /*
    void OnEnable()
    {

    }

    void OnDisable()
    {
        
    }
    */
}

