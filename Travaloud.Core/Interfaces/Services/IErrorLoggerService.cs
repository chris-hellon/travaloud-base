namespace Travaloud.Core.Interfaces.Services
{
    public interface IErrorLoggerService
    {
        void LogError(Exception ex);
        void LogError(string message);
        void LogError(Exception ex, object model);
        void LogInfo(string message);
    }
}