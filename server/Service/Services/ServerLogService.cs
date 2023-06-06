using Service.IServices;
using Service.Services.Util;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

namespace Service.Services;

public class ServerLogService : IServerLogService
{
    public List<string> GetLatestServerLog(string serverName)
    {
        return ReadLatestServerLog(serverName);
    }

    private List<string> ReadLatestServerLog(string serverName)
    {
        var logFile = $"{PersistenceUtil.GetServerPath(serverName)}\\logs\\latest.log";
        List<string> lines = new List<string>();
        using (var stream = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
        }   
        return lines;
    }
}
