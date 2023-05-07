using Domain;
using Service.InputModels;
using Service.IServices;
using System.Diagnostics;

namespace Service.Services;

public class ServerCreationService : IServerCreationService
{
    public void CreateServer(ServerInputModel serverInput)
    {
        string serverPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            $"CubeManager\\{serverInput.serverName}\\"
        );

        if (Directory.Exists(serverPath))
        {
            //throw new Exception("Server Folder already exists");
        }
        else
        {
            Directory.CreateDirectory(serverPath);
        }

        ProcessStartInfo startProcessInfo = new ProcessStartInfo();
        startProcessInfo.RedirectStandardInput = true;
        startProcessInfo.WorkingDirectory = serverPath;
        startProcessInfo.FileName = "java";
        startProcessInfo.Arguments = $"-Xmx{serverInput.maxMemory}M -jar {serverPath}\\server.jar nogui";

        Process serverProcess = new Process();
        serverProcess.StartInfo = startProcessInfo;
        serverProcess.Start();
    }
}
