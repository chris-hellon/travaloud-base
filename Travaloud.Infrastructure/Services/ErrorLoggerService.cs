using Travaloud.Core.Interfaces.Services;
using Rollbar;
namespace Travaloud.Infrastructure.Services
{
    public class ErrorLoggerService : IErrorLoggerService
    {
        public void LogError(Exception ex)
        {
            //Log the error to your error database
            RollbarLocator.RollbarInstance.Error(ex);
        }

        public void LogError(string message)
        {
            RollbarLocator.RollbarInstance.Error(message);
        }

        public void LogError(Exception ex, object model)
        {
            RollbarLocator.RollbarInstance.Error(ex, new Dictionary<string, object>()
            {
                { "HostelBooking", model }
            });
        }

        public void LogInfo(string message)
        {
            RollbarLocator.RollbarInstance.Info(message);
        }
    }
}

