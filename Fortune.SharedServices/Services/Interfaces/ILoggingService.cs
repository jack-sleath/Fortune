namespace Fortune.Shared.Services.Interfaces
{
    public interface ILoggingService
    {
        void LogCritical(Exception exception);
        void LogCritical(string message);
        void LogError(Exception exception);
        void LogError(string message);
        void LogInfo(Exception exception);
        void LogInfo(string message);
        void LogWarning(Exception exception);
        void LogWarning(string message);
    }
}