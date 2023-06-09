namespace Managers;

public static class LogManager {
    public static string LogError(Exception e) {
        string logDir = "logs/";
        string logFile = $"{logDir}{DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss")}.log";
        if (!Directory.Exists(logDir))
            Directory.CreateDirectory(logDir);
        using (var writer = new StreamWriter(logFile)) {
            writer.Write(e.ToString());
        }
        return logFile;
    }
}
