using System;
using UnityEngine;

namespace Core.Logging
{
    public class Logger<T> : ILogger<T>
    {
        public void Log(string message)
        {
            Debug.Log(FormatedMessage(message));
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(FormatedMessage(message));
        }
        
        public void LogError(string message)
        {
            Debug.LogError(FormatedMessage(message));
        }

        public void LogError(Exception exception)
        {
            Debug.LogError(exception);
        }

        private static string FormatedMessage(string message) => $"[{typeof(T).Name}] {message}";
    }
}