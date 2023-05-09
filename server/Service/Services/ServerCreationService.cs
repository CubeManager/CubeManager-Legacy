using Service.InputModels;
using Service.IServices;
using System;
using System.Diagnostics;
using System.Net.WebSockets;

namespace Service.Services;

public class ServerCreationService : IServerCreationService
{
    public async void CreateServer(ServerInputModel serverInput)
    {
        var serverPath = GetServerPath(serverInput.serverName);

        if (Directory.Exists(serverPath))
        {
            //throw new Exception("Server Folder already exists");
        }
        else
        {
            Directory.CreateDirectory(serverPath);
        }

        var processStartInfo = CreateServerProcessStartInfo(serverPath, serverInput.maxMemory);

        var initialProcess = StartServerProcess(processStartInfo);
        await WaitForEulaFileCreationAsync(initialProcess);
        initialProcess.Kill();
        await initialProcess.WaitForExitAsync();
        AcceptEulaFile(serverPath);
        var primaryProcess = StartServerProcess(processStartInfo);
    }

    private ProcessStartInfo CreateServerProcessStartInfo(string serverPath, int maxMemory)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.RedirectStandardInput = true;
        processStartInfo.RedirectStandardOutput = true;
        processStartInfo.WorkingDirectory = serverPath;
        processStartInfo.FileName = "java";
        processStartInfo.Arguments = $"-Xmx{maxMemory}M -jar {serverPath}\\server.jar nogui";
        return processStartInfo;
    }

    private Process StartServerProcess(ProcessStartInfo startProcessInfo)
    {
        Process serverProcess = new Process();
        serverProcess.StartInfo = startProcessInfo;
        serverProcess.Start();
        return serverProcess;
    }

    private async Task WaitForEulaFileCreationAsync(Process process)
    {
        string line;
        while ((line = await process.StandardOutput.ReadLineAsync()) != null)
        {
            if (line.Contains("agree") && line.Contains("EULA"))
            {
                break;
            }
        }
    }

    private void AcceptEulaFile(string serverPath)
    {
        var eula = File.ReadAllText($"{serverPath}\\eula.txt");
        eula = eula.Replace("false", "true");
        File.WriteAllText($"{serverPath}\\eula.txt", eula);
    }

    private string GetServerPath(string serverName)
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            $"CubeManager\\{serverName}\\"
        );
    }
}
