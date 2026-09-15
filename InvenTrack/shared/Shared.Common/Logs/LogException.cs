using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace Shared.Common.Logs
{
    public class LogException
    {
        // Centralized structured exception logging.
        public static void LogExceptions(Exception ex, string? correlationId = null)
        {
            var msg = ex.ToString();
            if (!string.IsNullOrWhiteSpace(correlationId))
            {
                Log.ForContext("CorrelationId", correlationId)
                   .Error(ex, "Unhandled exception (CorrelationId: {CorrelationId})", correlationId);
            }
            else
            {
                Log.Error(ex, "Unhandled exception");
            }
        }

        public static void LogToFile(string message) => Log.Information(message);
        public static void LogToConsole(string message) => Log.Warning(message);
        public static void LogToDebugger(string message) => Log.Debug(message);
    }
}
