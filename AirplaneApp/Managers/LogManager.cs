namespace Managers;

public static class LogManager {
    private static readonly string _startTimeString;
    private static readonly string _dateTimeFormat = "yyyy-MM-dd-HH=mm=ss";

    static LogManager() {
        _startTimeString = DateTime.Now.ToString(_dateTimeFormat);
    }

    private static StreamWriter _GetFile(string fileName, bool append = false) {
        string logDir = "logs/";
        string logFile = $"{logDir}{fileName}";
        if (!Directory.Exists(logDir))
            Directory.CreateDirectory(logDir);
        return new StreamWriter(logFile, append);
    }

    public static string LogMessage(string msg) {
        string logFile = _startTimeString + ".log";
        using (StreamWriter writer = _GetFile(logFile, true)) {
            writer.WriteLine(msg);
        }
        return logFile;
    }

    public static string LogError(Exception exc) {
        string logFile = $"{DateTime.Now.ToString(_dateTimeFormat)}.log";
        using (StreamWriter writer = _GetFile(logFile)) {
            writer.Write(exc.ToString());
        }
        return logFile;
    }
}
