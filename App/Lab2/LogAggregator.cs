using System.Globalization;
using System.Text;

namespace App.Lab2;

public class LogAggregator
{
    private record LogRecord(DateTime timeStamp, string serviceName, string message);
    
    public static string MergeLogs(params string[] logLines)
    {
        var groups = new Dictionary<string, List<LogRecord>>();

        foreach (var logLine in logLines)
        {
            var splitted = logLine.Split(']', 3);
            if (splitted.Length != 3)
            {
                return String.Empty;
            }

            try
            {
                var timeStamp = splitted[0].Substring(1);
                var dateTime = DateTime.ParseExact(timeStamp, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                var traceId = splitted[1].Substring(splitted[1].IndexOf('[') + 1);

                var logText = splitted[2].Split(':', 2);
                var serviceName = logText[0].Trim();
                var message = logText[1];
                
                if (!groups.ContainsKey(traceId))
                {
                    groups[traceId] = new List<LogRecord>();
                }
                groups[traceId].Add(new LogRecord(dateTime, serviceName, message));
            }
            catch (IndexOutOfRangeException)
            {
                return String.Empty;
            }
        }

        var result = new StringBuilder();
        foreach (var kv in groups)
        {
            kv.Value.Sort((x, y) =>
            {
                var res = x.timeStamp.CompareTo(y.timeStamp);
                if (res == 0)
                {
                    return String.Compare(x.serviceName, y.serviceName,  StringComparison.Ordinal);
                }
                return res;
            });

            result.Append($"--- TraceId: [{kv.Key}] ---\n");
            foreach (var logRecord in kv.Value)
            {
                var timeStr = logRecord.timeStamp.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                result.Append($"[{timeStr}] {logRecord.serviceName}:{logRecord.message}\n");
            }
            result.Append('\n');
        }
        
        return result.ToString().Trim();
    }
}