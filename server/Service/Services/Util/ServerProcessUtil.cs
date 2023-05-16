
using System.Diagnostics;

namespace Service.Services.Util;

public static class ServerProcessUtil
{
    public static Process StartServerProcess(ProcessStartInfo startProcessInfo)
    {
        Process serverProcess = new Process();
        serverProcess.StartInfo = startProcessInfo;
        serverProcess.Start();
        return serverProcess;
    }

    public static ProcessStartInfo CreateServerProcessStartInfo(string serverPath, string serverJarFile, int maxMemory)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.RedirectStandardInput = true;
        processStartInfo.RedirectStandardOutput = true;
        processStartInfo.WorkingDirectory = serverPath;
        processStartInfo.FileName = "java";
        processStartInfo.Arguments = $"-Xmx{maxMemory}M -jar {Path.Combine(serverPath, serverJarFile)} nogui";
        return processStartInfo;
    }
}
