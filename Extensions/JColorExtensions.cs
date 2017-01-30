using System;
using UnityEngine;

public static class JColorExtensions
{
    public static Color Init(this Color color, string hex)
    {
        if (hex.Length == 3) {
            hex += hex;
        }
        float[] vals = { 0, 0, 0, 0 };
        for (int i = 0; i < 4; i++) {
            if (i * 2 >= hex.Length) {
                vals[i] = 1;
            } else {
                vals[i] += Convert.ToInt32(hex[i * 2].ToString(), 16) * 16;
                vals[i] += Convert.ToInt32(hex[i * 2 + 1].ToString(), 16);
                vals[i] /= 16 * 16;
            }
        }
        color = new Color(vals[0], vals[1], vals[2], vals[3]);
        return color;
    }
}