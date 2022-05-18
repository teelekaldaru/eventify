using System;
using System.Security.Principal;
using log4net;

namespace Eventify.Common.Utils.Logger
{
    public interface IActionLogger
    {
        void Action(string action, IIdentity identity, ActionSeverity severity, string message, string requestBody = null);
    }

    internal class EverywhereLogger : ILogger, IActionLogger
    {
        private readonly ILog _log4NetLogger;

        public EverywhereLogger()
        {
            _log4NetLogger = LogManager.GetLogger("Eventify");
        }

        public void Debug(string message)
        {
            var logMessage = GetMessage(message);
            _log4NetLogger.Debug(logMessage);
        }

        public void Info(string category, string message, bool logEvent = false)
        {
            var logMessage = GetMessage(category, message);
            _log4NetLogger.Info(logMessage);
        }

        public void Warning(string category, string message)
        {
            var logMessage = GetMessage(category, message);
            _log4NetLogger.Warn(logMessage);
        }

        public void Error(string category, string message, string? stackTrace = null)
        {
            var logMessage = GetMessage(category, message, stackTrace);
            _log4NetLogger.Error(logMessage);
        }

        public void Action(string action, IIdentity identity, ActionSeverity severity, string message, string requestBody)
        {
            var messageToLog = $"[{identity.Name ?? "Anonymous user"}] {message}";
            switch (severity)
            {
                case ActionSeverity.Normal:
                    Info(action, messageToLog, true);
                    break;
                case ActionSeverity.Dangerous:
                    Warning(action, messageToLog);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
            }
        }

        private static string GetMessage(string category, string? message = null, string? stackTrace = null)
        {
            return $"{category} - {message ?? string.Empty}{(stackTrace != null ? $"\n{stackTrace}" : null)}";
        }
    }

    public enum ActionSeverity
    {
        Normal,
        Dangerous
    }
}