namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Management.Automation;
using Service.IServices;
using Service.InputModels;


[ApiController]
[Route("servers")]
public class ServerController : ControllerBase { 

    [HttpGet]
    public string GetAll()
    {
        // Server server = new Server;
        // server.serverproperties = new ServerProperties();

        Process[] processCollection = Process.GetProcesses();
        Console.WriteLine(ProcessesUsingPorts(25565));
        // foreach (Process p in processCollection)
        // {
        //     Console.WriteLine(p.ProcessName);
        // }
        //Console.WriteLine(Process.GetProcessesByName("Vmmem")[0].ProcessName + " " + await GetCpuUsageForProcess(Process.GetProcessesByName("Vmmem")[0]) + "%");
        //lol("Vmmem");
            return string.Join("/n", (object[])(typeof(Process).GetProperties()));
    }
    private readonly IServerCreationService serverCreationService;

    public ServerController(IServerCreationService serverCreationService)
    {
        this.serverCreationService = serverCreationService;
    }

    [HttpPost]
    public ActionResult CreateServer([FromBody] ServerInputModel serverInput)
    {
        serverCreationService.CreateServer(serverInput);
        return Ok();
            }

    private async Task<double> GetCpuUsageForProcess(Process process)
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = process.TotalProcessorTime; await Task.Delay(500);
        var endTime = DateTime.UtcNow;
        var endCpuUsage = process.TotalProcessorTime; var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds; var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        Console.WriteLine(cpuUsageTotal * 100);
        return cpuUsageTotal * 100;
    }


    private static DateTime lastTime;
    private static TimeSpan lastTotalProcessorTime;
    private static DateTime curTime;
    private static TimeSpan curTotalProcessorTime;

    private void lol(string processName)
    {

        Console.WriteLine("Press the any key to stop...\n");
        while (!Console.KeyAvailable)
        {
            Process[] pp = Process.GetProcessesByName(processName);
            if (pp.Length == 0)
            {
                Console.WriteLine(processName + " does not exist");
            }
            else
            {
                Process p = pp[0];
                if (lastTime == null || lastTime == new DateTime())
                {
                    lastTime = DateTime.Now;
                    lastTotalProcessorTime = p.TotalProcessorTime;
                }
                else
                {
                    curTime = DateTime.Now;
                    curTotalProcessorTime = p.TotalProcessorTime;

                    double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                    Console.WriteLine("{0} CPU: {1:0.0}%", processName, CPUUsage * 100);

                    lastTime = curTime;
                    lastTotalProcessorTime = curTotalProcessorTime;
                }
            }

            Thread.Sleep(250);
        }
    }

    // ===============================================
// The Method That Parses The NetStat Output
// And Returns A List Of Port Objects
// ===============================================
//source: https://gist.github.com/cheynewallace/5971686

// public static List<ServerMinecraft> GetNetStatPorts()
// {
//   var ServersMinecraft = new List<ServerMinecraft>();

//   try {
//     using (Process p = new Process()) {

//       Process[] processCollection =  Process.GetProcessesByName("java");
//               foreach (Process proc in processCollection)
//         {
//             Console.WriteLine(proc.Id + " " + proc.ProcessName);
//         }


//       ProcessStartInfo ps = new ProcessStartInfo();
//       ps.Arguments = "-a -n -o";
//       ps.FileName = "netstat.exe";
//       ps.UseShellExecute = false;
//       ps.WindowStyle = ProcessWindowStyle.Hidden;
//       ps.RedirectStandardInput = true;
//       ps.RedirectStandardOutput = true;
//       ps.RedirectStandardError = true;

//       p.StartInfo = ps;
//       p.Start();

//       StreamReader stdOutput = p.StandardOutput;
//       StreamReader stdError = p.StandardError;

//       string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
//       string exitStatus = p.ExitCode.ToString();
      
//       if (exitStatus != "0") {
//         // Command Errored. Handle Here If Need Be
//       }

//       //Get The Rows
//       string[] rows = Regex.Split(content, "\r\n");
//       foreach (string row in rows) {
//         //Split it baby
//         string[] tokens = Regex.Split(row, "\\s+");
//         if (tokens.Length > 4 && (tokens[1].Equals("UDP") || tokens[1].Equals("TCP"))) {
//                     // Console.WriteLine(tokens[1] + " " + tokens[2] + " " + tokens[3] + " " + tokens[4]);
//           string localAddress = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1");
//           ServersMinecraft.Add(new ServerMinecraft {
//             port_number = localAddress.Split(':')[1],
//             protocol = tokens[1],
//             process_id = tokens[1] == "UDP" ? LookupProcess(Convert.ToInt16(tokens[4])) : LookupProcess(Convert.ToInt16(tokens[5]))
//           });
//         }
//       }
//       IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

// IPEndPoint[] endPoints = ipProperties.GetActiveTcpListeners();
// TcpConnectionInformation[] tcpConnections = 
//     ipProperties.GetActiveTcpConnections();

// foreach (TcpConnectionInformation info in tcpConnections)
// {
//     Console.WriteLine("Local: {0}:{1}\nRemote: {2}:{3}\nState: {4}\n", 
//         info.LocalEndPoint.Address, info.LocalEndPoint.Port,
//         info.RemoteEndPoint.Address, info.RemoteEndPoint.Port,
//         info.State.ToString());
// }
// Console.ReadLine();
//     }
//   } 
//   catch (Exception ex) 
//   { 
//     Console.WriteLine(ex.Message);
//   }
//   return ServersMinecraft;
// }

// public static string LookupProcess(int pid) 
// {
//   string procName;
//   try { procName = Process.GetProcessById(pid).ProcessName; } 
//   catch (Exception) { procName = "-";}
//   return procName;
// }

// // ===============================================
// // The Port Class We're Going To Create A List Of
// // ===============================================
// public class ServerMinecraft
// {
//   public string name
//   {
//     get
//     {
//       return string.Format("{0} ({1} port {2})",this.process_id, this.protocol, this.port_number);
//     }
//     set { }
//   }
//   public string port_number { get; set; }
//   public string process_id { get; set; }

//   public string protocol { get; set; }
// }

private static IEnumerable<uint> ProcessesUsingPorts(uint id)
{
    PowerShell ps = PowerShell.Create();
    ps.AddCommand("Get-NetTCPConnection").AddParameter("LocalPort", id);
    return ps.Invoke().Select(p => (uint)p.Properties["OwningProcess"].Value);
}

}
