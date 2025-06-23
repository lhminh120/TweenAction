
using UnityEngine;

public class Utilities
{
    #region Check Distance
    public static bool CheckDistance(Vector3 a, Vector3 b, float distance, bool checkSmaller = true)
    {
        float ab = (a - b).sqrMagnitude;
        return checkSmaller ? ab <= distance * distance : ab >= distance * distance;
    }
    public static bool CheckDistance(Vector2 a, Vector2 b, float distance, bool checkSmaller = true)
    {
        float ab = (a - b).sqrMagnitude;
        return checkSmaller ? ab <= distance * distance : ab >= distance * distance;
    }
    public static bool CheckDistance(Vector3 a, float distance, bool checkSmaller = true)
    {
        float ab = a.sqrMagnitude;
        return checkSmaller ? ab <= distance * distance : ab >= distance * distance;
    }
    public static bool CheckDistance(Vector2 a, float distance, bool checkSmaller = true)
    {
        float ab = a.sqrMagnitude;
        return checkSmaller ? ab <= distance * distance : ab >= distance * distance;
    }
    #endregion
    public static float CalculateTotalStatsBaseOnLevel(float basicStat, float addUpStatEveryLevel, int level, float bonusStatNumber = 0, float bonusStatPercent = 0)
    {
        return (basicStat + addUpStatEveryLevel * level) * (bonusStatPercent + 1) + bonusStatNumber;
    }
    public static void Translate(ref Vector3 startPos, Vector3 reachPos, float speed)
    {
        Vector3 temp = startPos;
        double length = reachPos.magnitude;
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            temp.x *= tempValue;
            temp.y *= tempValue;
            temp.z *= tempValue;
        }
        startPos.x += temp.x * speed;
        startPos.y += temp.y * speed;
        startPos.z += temp.z * speed;
    }
    #region Smooth Progress
    public static float Smooth(GlobalVariables.LeanEase ease, float x)
    {
        switch (ease)
        {
            case GlobalVariables.LeanEase.Smooth:
                {
                    x = x * x * (3.0f - 2.0f * x);
                }
                break;

            case GlobalVariables.LeanEase.Accelerate:
                {
                    x *= x;
                }
                break;

            case GlobalVariables.LeanEase.Decelerate:
                {
                    x = 1.0f - x;
                    x *= x;
                    x = 1.0f - x;
                }
                break;

            case GlobalVariables.LeanEase.Elastic:
                {
                    var angle = x * Mathf.PI * 4.0f;
                    var weightA = 1.0f - Mathf.Pow(x, 0.125f);
                    var weightB = 1.0f - Mathf.Pow(1.0f - x, 8.0f);

                    x = Mathf.LerpUnclamped(0.0f, 1.0f - Mathf.Cos(angle) * weightA, weightB);
                }
                break;

            case GlobalVariables.LeanEase.Back:
                {
                    x = 1.0f - x;
                    x = x * x * x - x * Mathf.Sin(x * Mathf.PI);
                    x = 1.0f - x;
                }
                break;

            case GlobalVariables.LeanEase.Bounce:
                {
                    if (x < (4f / 11f))
                    {
                        x = (121f / 16f) * x * x;
                    }
                    else if (x < (8f / 11f))
                    {
                        x = (121f / 16f) * (x - (6f / 11f)) * (x - (6f / 11f)) + 0.75f;
                    }
                    else if (x < (10f / 11f))
                    {
                        x = (121f / 16f) * (x - (9f / 11f)) * (x - (9f / 11f)) + (15f / 16f);
                    }
                    else
                    {
                        x = (121f / 16f) * (x - (21f / 22f)) * (x - (21f / 22f)) + (63f / 64f);
                    }
                }
                break;

            case GlobalVariables.LeanEase.SineIn: return 1 - Mathf.Cos((x * Mathf.PI) / 2.0f);

            case GlobalVariables.LeanEase.SineOut: return Mathf.Sin((x * Mathf.PI) / 2.0f);

            case GlobalVariables.LeanEase.SineInOut: return -(Mathf.Cos(Mathf.PI * x) - 1.0f) / 2.0f;

            case GlobalVariables.LeanEase.QuadIn: return SmoothQuad(x);

            case GlobalVariables.LeanEase.QuadOut: return 1 - SmoothQuad(1 - x);

            case GlobalVariables.LeanEase.QuadInOut: return x < 0.5f ? SmoothQuad(x * 2) / 2 : 1 - SmoothQuad(2 - x * 2) / 2;

            case GlobalVariables.LeanEase.CubicIn: return SmoothCubic(x);

            case GlobalVariables.LeanEase.CubicOut: return 1 - SmoothCubic(1 - x);

            case GlobalVariables.LeanEase.CubicInOut: return x < 0.5f ? SmoothCubic(x * 2) / 2 : 1 - SmoothCubic(2 - x * 2) / 2;

            case GlobalVariables.LeanEase.QuartIn: return SmoothQuart(x);

            case GlobalVariables.LeanEase.QuartOut: return 1 - SmoothQuart(1 - x);

            case GlobalVariables.LeanEase.QuartInOut: return x < 0.5f ? SmoothQuart(x * 2) / 2 : 1 - SmoothQuart(2 - x * 2) / 2;

            case GlobalVariables.LeanEase.QuintIn: return SmoothQuint(x);

            case GlobalVariables.LeanEase.QuintOut: return 1 - SmoothQuint(1 - x);

            case GlobalVariables.LeanEase.QuintInOut: return x < 0.5f ? SmoothQuint(x * 2) / 2 : 1 - SmoothQuint(2 - x * 2) / 2;

            case GlobalVariables.LeanEase.ExpoIn: return SmoothExpo(x);

            case GlobalVariables.LeanEase.ExpoOut: return 1 - SmoothExpo(1 - x);

            case GlobalVariables.LeanEase.ExpoInOut: return x < 0.5f ? SmoothExpo(x * 2) / 2 : 1 - SmoothExpo(2 - x * 2) / 2;

            case GlobalVariables.LeanEase.CircIn: return SmoothCirc(x);

            case GlobalVariables.LeanEase.CircOut: return 1 - SmoothCirc(1 - x);

            case GlobalVariables.LeanEase.CircInOut: return x < 0.5f ? SmoothCirc(x * 2) / 2 : 1 - SmoothCirc(2 - x * 2) / 2;

            case GlobalVariables.LeanEase.BackIn: return SmoothBack(x);

            case GlobalVariables.LeanEase.BackOut: return 1 - SmoothBack(1 - x);

            case GlobalVariables.LeanEase.BackInOut: return x < 0.5f ? SmoothBack(x * 2) / 2 : 1 - SmoothBack(2 - x * 2) / 2;

            case GlobalVariables.LeanEase.ElasticIn: return SmoothElastic(x);

            case GlobalVariables.LeanEase.ElasticOut: return 1 - SmoothElastic(1 - x);

            case GlobalVariables.LeanEase.ElasticInOut: return x < 0.5f ? SmoothElastic(x * 2) / 2 : 1 - SmoothElastic(2 - x * 2) / 2;

            case GlobalVariables.LeanEase.BounceIn: return 1 - SmoothBounce(1 - x);

            case GlobalVariables.LeanEase.BounceOut: return SmoothBounce(x);

            case GlobalVariables.LeanEase.BounceInOut: return x < 0.5f ? 0.5f - SmoothBounce(1 - x * 2) / 2 : 0.5f + SmoothBounce(x * 2 - 1) / 2;
        }

        return x;
    }

    private static float SmoothQuad(float x)
    {
        return x * x;
    }

    private static float SmoothCubic(float x)
    {
        return x * x * x;
    }

    private static float SmoothQuart(float x)
    {
        return x * x * x * x;
    }

    private static float SmoothQuint(float x)
    {
        return x * x * x * x * x;
    }

    private static float SmoothExpo(float x)
    {
        return x == 0.0f ? 0.0f : Mathf.Pow(2.0f, 10.0f * x - 10.0f);
    }

    private static float SmoothCirc(float x)
    {
        return 1.0f - Mathf.Sqrt(1.0f - Mathf.Pow(x, 2.0f));
    }

    private static float SmoothBack(float x)
    {
        return 2.70158f * x * x * x - 1.70158f * x * x;
    }

    private static float SmoothElastic(float x)
    {
        return x == 0.0f ? 0.0f : x == 1.0f ? 1.0f : -Mathf.Pow(2.0f, 10.0f * x - 10.0f) * Mathf.Sin((x * 10.0f - 10.75f) * ((2.0f * Mathf.PI) / 3.0f));
    }

    private static float SmoothBounce(float x)
    {
        if (x < (4f / 11f))
        {
            return (121f / 16f) * x * x;
        }
        else if (x < (8f / 11f))
        {
            return (121f / 16f) * (x - (6f / 11f)) * (x - (6f / 11f)) + 0.75f;
        }
        else if (x < (10f / 11f))
        {
            return (121f / 16f) * (x - (9f / 11f)) * (x - (9f / 11f)) + (15f / 16f);
        }
        else
        {
            return (121f / 16f) * (x - (21f / 22f)) * (x - (21f / 22f)) + (63f / 64f);
        }
    }
    #endregion
    #region  Smooth number
    public static Vector3 SmoothVector3(Vector3 original, Vector3 target, float progress, GlobalVariables.LeanEase ease = GlobalVariables.LeanEase.Smooth)
    {
        Vector3 temp = target - original;
        temp *= Smooth(ease, progress);
        return original + temp;
    }
    public static Color SmoothColor(Color colorOriginal, Color colorTarget, float progress, GlobalVariables.LeanEase ease = GlobalVariables.LeanEase.Smooth)
    {
        Color color = colorTarget - colorOriginal;
        color *= Smooth(ease, progress);
        return colorOriginal + color;
    }
    #endregion



}
