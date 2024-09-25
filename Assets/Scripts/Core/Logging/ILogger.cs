using System;

namespace Core.Logging
{
    public interface ILogger<T>
    {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(Exception exception);
    }
}