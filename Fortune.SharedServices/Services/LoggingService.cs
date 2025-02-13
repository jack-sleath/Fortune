using Fortune.Shared.Models.Enums;
using Fortune.Shared.Services.Interfaces;

namespace Fortune.Shared.Services
{
    public class LoggingService : ILoggingService
    {
        public void LogInfo(Exception exception)
        {
            Log(exception, ELoggingSeverity.Info);
        }
        public void LogInfo(string message)
        {
            Log(message, ELoggingSeverity.Info);
        }
        public void LogWarning(Exception exception)
        {
            Log(exception, ELoggingSeverity.Warning);
        }
        public void LogWarning(string message)
        {
            Log(message, ELoggingSeverity.Warning);
        }
        public void LogError(Exception exception)
        {
            Log(exception, ELoggingSeverity.Error);
        }
        public void LogError(string message)
        {
            Log(message, ELoggingSeverity.Error);
        }
        public void LogCritical(Exception exception)
        {
            Log(exception, ELoggingSeverity.Critical);
        }
        public void LogCritical(string message)
        {
            Log(message, ELoggingSeverity.Critical);
        }
        private void Log(Exception exception, ELoggingSeverity severity)
        {
            Log(exception.Message, severity);
        }

        private void Log(string message, ELoggingSeverity severity)
        {
            string logEntry = $"{DateTime.Now:HH:mm:ss} - {message}.";

            if (severity >= ELoggingSeverity.Critical)
            {

            }

            if (severity >= ELoggingSeverity.Error)
            {

            }

            if (severity >= ELoggingSeverity.Warning)
            {
                string folderPath = "Output/Logs"; // Change this to your desired folder
                Directory.CreateDirectory(folderPath); // Ensure the directory exists

                string fileName = $"{DateTime.Today:yyyy-MM-dd}.txt"; // Format: YYYY-MM-DD.txt
                string filePath = Path.Combine(folderPath, fileName);

                // Append to the file (creates if it doesn't exist)
                File.AppendAllText(filePath, logEntry + Environment.NewLine);
            }

            if (severity >= ELoggingSeverity.Info)
            {
                Console.WriteLine(logEntry);
            }
        }
    }
}
