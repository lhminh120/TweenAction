using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NMath
{
     #region Round
    public static long Round(float a)
    {
        return a >= 0 ? (long)(a + 0.5f) : (long)(a - 0.5f);
    }
    public static long Round(double a)
    {
        return a >= 0 ? (long)(a + 0.5f) : (long)(a - 0.5f);
    }
    #endregion
    #region Abs
    public static int Abs(int a)
    {
        if (a < 0) return -a;
        return a;
    }
    public static long Abs(long a)
    {
        if (a < 0) return -a;
        return a;
    }
    public static double Abs(double a)
    {
        if (a < 0) return -a;
        return a;
    }
    public static float Abs(float a)
    {
        if (a < 0) return -a;
        return a;
    }
    #endregion
}
