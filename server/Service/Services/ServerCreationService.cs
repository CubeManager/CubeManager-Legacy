namespace Service.Services;

using Service.InputModels;
using Service.IServices;
using System.Diagnostics;

public class ServerCreationService : IServerCreationService
{
    IServerPropertiesService serverPropertiesService;

    public ServerCreationService(IServerPropertiesService serverPropertiesService)
    {
        this.serverPropertiesService = serverPropertiesService;
    }

    public async Task CreateServer(ServerInputModel serverInput)
    {
        var serverPath = Util.GetServerPath(serverInput.serverName);

        if(serverInput.maxMemory < 250)
        {
            // throw new Exception("maxMemory must be at least 250 MB")
        }

        if (Directory.Exists(serverPath))
        {
            //throw new Exception("Server Folder already exists");
        }

        Directory.CreateDirectory(serverPath);

        var processStartInfo = CreateServerProcessStartInfo(serverPath, serverInput.maxMemory);

        CreateEulaFile(serverPath);

        var primaryProcess = StartServerProcess(processStartInfo);
        await WaitUntilDoneAsync(primaryProcess);
        primaryProcess.Kill();
        await primaryProcess.WaitForExitAsync();
        if (serverInput.serverProperties != null)
        {
            serverPropertiesService.ChangeServerProperties(serverInput.serverProperties, serverInput.serverName);
        }
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

    private async Task WaitUntilDoneAsync(Process process)
    {
        string line;
        while ((line = await process.StandardOutput.ReadLineAsync()) != null)
        {
            if (line.Contains("Done") && line.Contains("!"))
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

    private void CreateEulaFile(string serverPath)
    {
        File.WriteAllText(Path.Combine(serverPath, "eula.txt"), $"#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://aka.ms/MinecraftEULA).\r\n#{DateTime.Now}\r\neula=true\r\n");
    }
}
