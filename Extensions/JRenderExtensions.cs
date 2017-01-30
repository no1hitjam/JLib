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
        float width = 400,
        float height = 200,
        int font_size = 100,
        string font = "Arial",
        Color? color = null,
        TextAnchor alignment = TextAnchor.MiddleCenter,
        JLib.TextFitMode? fit = null)
    {
        text = new GameObject().AddComponent<Text>();
        text.transform.SetParent(parent, false);

        text.text = content;
        text.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        text.fontSize = font_size;
        //text.font = Font(font);
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
                    text.Add<RectShrinker>().Init(width, JLib.Axes(true, false, false));
                }
            } else if (fit.Value == JLib.TextFitMode.ShrinkY || fit.Value == JLib.TextFitMode.StretchY) {
                fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                if (fit.Value == JLib.TextFitMode.ShrinkY) {
                    text.Add<RectShrinker>().Init(width, JLib.Axes(true, false, false));
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
        Sprite sprite = null, 
        Color? color = null,
        Vector2 size = default(Vector2), 
        Vector2 pivot = default(Vector2))
    {
        image.transform.SetParent(parent, false);
        image.sprite = sprite;
        if (color.HasValue) {
            image.color = color.Value;
        } else {
            image.color = Color.white;
        }
        image.SetSize(size);
        image.SetPivot(pivot);

        return image;
    }

    public static Animation AddAnimation(this Image image, List<Sprite> sprites)
    {
        return image.Add<Animation>().Init((s) => { image.sprite = s; }, sprites);
    }


    public static SpriteRenderer Init(
        this SpriteRenderer spriteRenderer,
        Transform parent,
        Sprite sprite = null,
        Color? color = null)
    {
        spriteRenderer.transform.SetParent(parent, false);
        spriteRenderer.sprite = sprite;
        if (color.HasValue) {
            spriteRenderer.color = color.Value;
        } else {
            spriteRenderer.color = Color.white;
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

