
using UnityEngine;

namespace KatSoftware.JetSim.Common.Runtime
{
    public static class JS_Debug
    {
        private const string _NAME = "JetSim";
        private const string _LOG_COLOR = "cyan";

        public static void Log(string message, Object context = null)
        {
            Debug.Log($"[<color={_LOG_COLOR}>{_NAME}</color>] <color=lightblue>{message}</color>", context);
        }

        public static void LogSuccess(string message, Object context = null)
        {
            Debug.Log($"[<color={_LOG_COLOR}>{_NAME}</color>] <color=lime>{message}</color>", context);
        }

        public static void LogWarning(string message, Object context = null)
        {
            Debug.LogWarning($"[<color={_LOG_COLOR}>{_NAME}</color>] <color=orange>{message}</color>", context);
        }

        public static void LogError(string message, Object context = null)
        {
            Debug.LogError($"[<color={_LOG_COLOR}>{_NAME}</color>] <color=red>{message}</color>", context);
        }
    }
}
