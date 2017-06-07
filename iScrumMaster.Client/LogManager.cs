using iScrumMaster.Models;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace iScrumMaster
{
    public static class LogManager
    {
        private static LoggingServiceClient loggingService = new LoggingServiceClient();        

        private static void LoggingService_LogCompleted(object sender, LogCompletedEventArgs e)
        {
            loggingService.LogCompleted -= LoggingService_LogCompleted;
        }

        public static void LogMessage(LogTypes type, string message)
        {
            loggingService.LogCompleted += LoggingService_LogCompleted;
            loggingService.LogAsync(type, message);
        }
    }
}
