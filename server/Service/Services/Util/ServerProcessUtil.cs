﻿
namespace Service.Services.Util;

using System.Diagnostics;

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
        processStartInfo.Arguments = $"-Xmx{maxMemory}M -jar {serverJarFile} nogui";
        return processStartInfo;
    }

    public static async void KillServerProcess(Process process)
    {
        if (!process.HasExited)
        {
            process.StandardInput.WriteLine("stop");
            await process.WaitForExitAsync();
            process.Close();
        }
    }

    public static async Task KillServerProcessAsync(Process process)
    {
        if (!process.HasExited)
        {
            process.StandardInput.WriteLine("stop");
            await process.WaitForExitAsync();
            process.Close();
        }
        return;
    }
}
