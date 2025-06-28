using UnityEngine;
public static class GlobalVariables
{
    public enum MaterialNormalName : int
    {
        YELLOW = 0,
        BLUE,
        RED,
        VIOLET,
        PINK,
        GREEN,
        WHITE,
        BROWN,
    }
    public enum MaterialDarkName : int
    {
        YELLOW_DARK = 0,
        BLUE_DARK,
        RED_DARK,
        VIOLET_DARK,
        PINK_DARK,
        GREEN_DARK,
        WHITE_DARK,
        BROWN_DARK
    }
    public enum LeanEase
    {
        // Basic
        Linear,
        Smooth = 100,
        Accelerate = 200,
        Decelerate = 250,
        Elastic = 300,
        Back = 400,
        Bounce = 500,

        // Advanced
        SineIn = 1000,
        SineOut,
        SineInOut,

        QuadIn = 1100,
        QuadOut,
        QuadInOut,

        CubicIn = 1200,
        CubicOut,
        CubicInOut,

        QuartIn = 1300,
        QuartOut,
        QuartInOut,

        QuintIn = 1400,
        QuintOut,
        QuintInOut,

        ExpoIn = 1500,
        ExpoOut,
        ExpoInOut,

        CircIn = 1600,
        CircOut,
        CircInOut,

        BackIn = 1700,
        BackOut,
        BackInOut,

        ElasticIn = 1800,
        ElasticOut,
        ElasticInOut,

        BounceIn = 1900,
        BounceOut,
        BounceInOut,
    }
}