﻿namespace Service.Services;

using Util;
using Service.InputModels;
using Service.IServices;
using System.Diagnostics;

public class ServerCreationService : IServerCreationService
{
    IServerPropertiesService serverPropertiesService;
    IServerCubeManagerConfigService serverCubeManagerConfigService;

    public ServerCreationService(IServerPropertiesService serverPropertiesService, IServerCubeManagerConfigService serverCubeManagerConfigService)
    {
        this.serverPropertiesService = serverPropertiesService;
        this.serverCubeManagerConfigService = serverCubeManagerConfigService;
    }

    public async Task CreateServer(ServerInputModel serverInput)
    {
        var serverPath = PersistenceUtil.GetServerPath(serverInput.serverName);

        if (serverInput.maxMemory < 250)
        {
            // throw new Exception("maxMemory must be at least 250 MB")
            serverInput.maxMemory = 250;
        }

        if (Directory.Exists(serverPath))
        {
            throw new Exception("Server Folder already exists");
        }

        Directory.CreateDirectory(serverPath);

        try
        {
            await FileCopyAsync($"{PersistenceUtil.GetApplicationPath()}serverjars\\{serverInput.serverFileName}", $"{serverPath}\\{serverInput.serverFileName}");
            Console.WriteLine("File copied successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying file: {ex.Message}");
        }

        var processStartInfo = ServerProcessUtil.CreateServerProcessStartInfo(serverPath, serverInput.serverFileName, serverInput.maxMemory);

        CreateEulaFile(serverPath);

        var primaryProcess = StartServerProcess(processStartInfo);
        await WaitUntilDoneAsync(primaryProcess);
        ServerProcessUtil.KillServerProcess(primaryProcess);
        if (serverInput.serverProperties != null)
        {
            serverPropertiesService.ChangeServerProperties(serverInput.serverProperties, serverInput.serverName);
        }

        CreateServerCubeManagerConfig(serverInput.serverFileName, serverInput.maxMemory, serverInput.serverName);
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

    private void CreateEulaFile(string serverPath)
    {
        File.WriteAllText(Path.Combine(serverPath, "eula.txt"), $"#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://aka.ms/MinecraftEULA).\r\n#{DateTime.Now}\r\neula=true\r\n");
    }

    private void CreateServerCubeManagerConfig(string jarFile, int maxMemory, string serverName)
    {
        var config = CubeManagerConfigUtil.CreateServerCubeManagerConfig(jarFile, maxMemory);
        CubeManagerConfigUtil.SetCubeManagerConfig(config, serverName);
    }

    private async Task FileCopyAsync(string sourcePath, string destinationPath)
    {
        using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
        {
            using (FileStream destinationStream = new FileStream(destinationPath, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }
        }
    }
}
