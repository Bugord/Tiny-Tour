using System;
using UnityEngine;

namespace Core.Logging
{
    public class Logger<T> : ILogger<T>
    {
        public void Log(object message)
        {
            Debug.Log(FormatedMessage(message.ToString()));
        }

        public void LogWarning(object message)
        {
            Debug.LogWarning(FormatedMessage(message.ToString()));
        }
        
        public void LogError(object message)
        {
            Debug.LogError(FormatedMessage(message.ToString()));
        }

        public void LogError(Exception exception)
        {
            Debug.LogError(exception);
        }

        private static string FormatedMessage(string message) => $"[{typeof(T).Name}] {message}";
    }
}