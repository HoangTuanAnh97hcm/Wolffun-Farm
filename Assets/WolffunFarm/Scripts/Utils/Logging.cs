using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logging
{
    public static Logger testLogger = new Logger(Debug.unityLogger.logHandler);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogMessage(object message)
    {
        Debug.Log($"{message}");
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(object message)
    {
        Debug.LogWarning($"{message}");
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(object message)
    {
        Debug.LogError($"{message}");
    }

    public static void LoadLoggers()
    {
        testLogger.logEnabled = true;
    }
}
