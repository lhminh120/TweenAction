using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebugKey
{
    None,
    Debugging,
    Default,
}
public class DebugClass
{
    private static bool _showAll = true;
    private static HashSet<DebugKey> _alwaysShowKey = new HashSet<DebugKey>
    {
        DebugKey.Debugging
    };
    private static HashSet<DebugKey> _debugKeyLog = new HashSet<DebugKey>
    {
        DebugKey.None
    };

    private static HashSet<DebugKey> _debugKeyLogError = new HashSet<DebugKey>
    {
        DebugKey.Default
    };
    private static HashSet<DebugKey> _debugKeyLogWarning = new HashSet<DebugKey>
    {
        DebugKey.Default
    };
    public static void Log(object obj, DebugKey debugKey = DebugKey.Default)
    {
        if (_debugKeyLog.Contains(debugKey) || _showAll || _alwaysShowKey.Contains(debugKey))
        {
            Debug.Log(obj);
        }
    }
    public static void LogError(object obj, DebugKey debugKey = DebugKey.Default)
    {
        if (_debugKeyLogError.Contains(debugKey) || _showAll || _alwaysShowKey.Contains(debugKey))
        {
            Debug.LogError(obj);
        }
    }
    public static void LogWarning(object obj, DebugKey debugKey = DebugKey.Default)
    {
        if (_debugKeyLogWarning.Contains(debugKey) || _showAll || _alwaysShowKey.Contains(debugKey))
        {
            Debug.LogWarning(obj);
        }
    }
}
