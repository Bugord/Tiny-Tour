using System;

namespace Core.Logging
{
    public interface ILogger<T>
    {
        void Log(object message);
        void LogWarning(object message);
        void LogError(object message);
        void LogError(Exception exception);
    }
}