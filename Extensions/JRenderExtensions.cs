using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class JRenderExtensions
{

    public static Vector2 GetPivot(this MaskableGraphic graphic)
    {
        return graphic.Get<RectTransform>().pivot;
    }

    public static void SetPivot(this MaskableGraphic graphic, Vector2 pivot)
    {
        graphic.Get<RectTransform>().pivot = pivot;
    }

    public static Vector2 GetSize(this MaskableGraphic graphic)
    {
        return graphic.Get<RectTransform>().sizeDelta;
    }

    public static void SetSize(this MaskableGraphic graphic, Vector2 size)
    {
        graphic.Get<RectTransform>().sizeDelta = size;
    }

    public static Text Init(
        this Text text,
        Transform parent,
        string content = "",
        Vector2 size = default(Vector2),
        int font_size = 50,
        string font = "Arial",
        Color? color = null,
        TextAnchor alignment = TextAnchor.MiddleCenter,
        JLib.TextFitMode? fit = null)
    {
        text.transform.SetParent(parent, false);

        text.text = content;
        text.GetComponent<RectTransform>().sizeDelta = size;
        text.fontSize = font_size;
        //text.font = Font(font);
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.alignment = alignment;

        if (color.HasValue) {
            text.color = color.Value;
        } else {
            text.color = Color.black;
        }

        if (fit.HasValue) {
            var fitter = text.gameObject.AddComponent<ContentSizeFitter>();
            if (fit.Value == JLib.TextFitMode.ShrinkX || fit.Value == JLib.TextFitMode.StretchX) {
                fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                if (fit.Value == JLib.TextFitMode.ShrinkX) {
                    text.Add<RectShrinker>().Init(size.x, JLib.Axes(true, false, false));
                }
            } else if (fit.Value == JLib.TextFitMode.ShrinkY || fit.Value == JLib.TextFitMode.StretchY) {
                fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                if (fit.Value == JLib.TextFitMode.ShrinkY) {
                    text.Add<RectShrinker>().Init(size.x, JLib.Axes(true, false, false));
                }
            }
        } else {
            UnityEngine.Object.Destroy(text.Get<ContentSizeFitter>());
            UnityEngine.Object.Destroy(text.Get<RectShrinker>());
        }

        return text;
    }


    public static Image Init(
        this Image image,
        Transform parent,
        string name = null,
        Sprite sprite = null, 
        Color? color = null,
        Vector2 size = default(Vector2), 
        Vector2? pivot = null)
    {
        image.transform.SetParent(parent, false);
        if (name != null) {
            image.GetOrAdd<ID>().Init(name);
        }
        image.sprite = sprite;
        if (color.HasValue) {
            image.color = color.Value;
        }
        image.SetSize(size);
        if (pivot.HasValue) {
            image.SetPivot(pivot.Value);
        }

        return image;
    }

    public static Animation AddAnimation(this Image image, List<Sprite> sprites)
    {
        return image.Add<Animation>().Init((s) => { image.sprite = s; }, sprites);
    }


    public static SpriteRenderer Init(
        this SpriteRenderer spriteRenderer,
        Transform parent,
        string name = null,
        Sprite sprite = null,
        Color? color = null,
        int? sortingOrder = null)
    {
        spriteRenderer.transform.SetParent(parent, false);
        if (name != null) {
            spriteRenderer.GetOrAdd<ID>().Init(name);
        }
        spriteRenderer.sprite = sprite;
        if (color.HasValue) {
            spriteRenderer.color = color.Value;
        }
        if (sortingOrder.HasValue) {
            spriteRenderer.sortingOrder = sortingOrder.Value;
        }

        return spriteRenderer;
    }

    public static Animation AddAnimation(this SpriteRenderer spriteRenderer, List<Sprite> sprites)
    {
        return spriteRenderer.Add<Animation>().Init((s) => { spriteRenderer.sprite = s; }, sprites);
    }

    /*public static Font Init(string name)
    {

    }*/

}

