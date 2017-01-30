using System;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Action<Sprite> _setSprite;
    public List<Sprite> Sprites;
    private int _spriteIndex;

    public Animation Init(Action<Sprite> setSprite, List<Sprite> sprites)
    {
        _setSprite = setSprite;
        Sprites = sprites;
        return this;
    }

    public int SpriteIndex
    {
        get
        {
            if (Sprites.Count == 0) {
                Debug.LogWarning("Animation: No sprites to set");
                Debug.Break();
            }
            return _spriteIndex;
        }

        set
        {
            if (Sprites.Count == 0) {
                Debug.LogError("Animation: No sprites to set");
                Debug.Break();
                return;
            } else if (value < 0 || value > Sprites.Count) {
                Debug.LogError("Animation: Index [" + value + "] out of sprites' range");
                Debug.Break();
                return;
            }
            _spriteIndex = value;
            _setSprite(Sprites[_spriteIndex]);
        }
    }
}

