using System;
using System.Collections.Generic;

public static class JMath
{
    public static bool True(this float f)
    {
        return f != 0;
    }

    public static int Mod(int n, int mod)
    {
        while (n < 0)
            n += mod;
        return n % mod;
    }

    private static Dictionary<string, Random> rngs = new Dictionary<string, Random>();

    public static int Random(int min, int max, string rng_name = "default")
    {
        Random rng;
        if (rngs.ContainsKey(rng_name)) {
            rng = rngs[rng_name];
        } else {
            rng = new System.Random();
            rngs[rng_name] = rng;
        }
        return rng.Next(min, max);
    }

    public static float Random(float min = 0, float max = 1, string rng_name = "default")
    {
        Random rng;
        if (rngs.ContainsKey(rng_name)) {
            rng = rngs[rng_name];
        } else {
            rng = new Random();
            rngs[rng_name] = rng;
        }
        return rng.NextFloat() % (max - min) + min;
    }

    private static float NextFloat(this Random random)
    {
        double mantissa = (random.NextDouble() * 2.0) - 1.0;
        double exponent = Math.Pow(2.0, random.Next(-126, 128));
        return (float)(mantissa * exponent);
    }

    public static void SetRNG(string name, int? seed = null)
    {
        if (seed.HasValue) {
            rngs[name] = new Random(seed.Value);
        } else {
            rngs[name] = new Random();
        }
    }

    public static int Bound(int x, int low, int high)
    {
        if (x < low) {
            x = low;
        } else if (x > high) {
            x = high;
        }
        return x;
    }

    public static float Bound(float x, float low, float high)
    {
        if (x < low) {
            x = low;
        } else if (x > high) {
            x = high;
        }
        return x;
    }

    
}

