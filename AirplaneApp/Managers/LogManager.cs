namespace Managers;

public static class LogManager {
    public static string LogError(Exception e) => LogError(e.ToString());
    public static string LogError(string msg) {
        string logDir = "logs/";
        string logFile = $"{logDir}{DateTime.Now.ToString("yyyy-MM-dd-HH=mm=ss")}.log";
        if (!Directory.Exists(logDir))
            Directory.CreateDirectory(logDir);
        using (var writer = new StreamWriter(logFile)) {
            writer.Write(msg);
        }
        return logFile;
    }
}
