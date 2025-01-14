using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.IO.Ports;
using System.Text;

namespace Arduino_data_streamer
{
// {
//     public class SerialSender : BackgroundService
//     {
//         private readonly IHubContext<DataHub> _hubContext;
//         private readonly SerialPort _serial;
//         private readonly ILogger<SerialSender> _logger;

//         public SerialSender(IHubContext<DataHub> hubContext, ILogger<SerialSender> logger)
//         {
//             _hubContext = hubContext;
//             _logger = logger;
//             //var ports = SerialPort.GetPortNames();
//             //if (ports.Length == 0) throw new Exception("No discovered ports!");
//             //Console.WriteLine($"Available ports: {string.Join(",", ports)}");
//             //Console.Write("Please select port: ");
//             //var selectedPort = Console.ReadLine();
//             //if (!ports.Contains(selectedPort)) throw new Exception("Invalid port");


//             var serial = new SerialPort();
//             serial.BaudRate = 9600;
//             serial.PortName = "COM6";
//             serial.ReadTimeout = 1500;
//             serial.DataBits = 8;
//             serial.Parity = Parity.None;
//             serial.StopBits = StopBits.One;
//             serial.RtsEnable = true;
//             serial.DtrEnable = true;
//             serial.Encoding = Encoding.ASCII;
//             _serial = serial;  
//         }

//         private async Task DoWork(CancellationToken cancellationToken)
//         {
//             //using var serialStream = _serial.BaseStream;
//             while (!cancellationToken.IsCancellationRequested)
//             {
//                 await Task.Run(async () =>
//                 {
//                     string message = null;
//                     try
//                     {
//                         message = _serial.ReadLine();
//                     }
//                     catch (TimeoutException) { }

//                     if (!String.IsNullOrEmpty(message))
//                     {
//                         //_logger.LogInformation($"Sending message: {message}");

//                         var splitted = message.Split("|");
//                         if (splitted.Length < 5) return;
//                         try
//                         {
//                             var data = new DataModel()
//                             {
//                                 BotId = splitted[0],
//                                 SensorFrontDistance = int.Parse(splitted[2]),
//                                 SensorLeftDistance = int.Parse(splitted[2]),
//                                 SensorRightDistance = int.Parse(splitted[2]),
//                                 MotorsSpeed = int.Parse(splitted[3]),
//                                 IsOnLine = splitted[4] == "True" ? true : false,
//                             };
//                             data.Timestamp = DateTime.Now;
//                             await _hubContext.Clients.All.SendAsync("newData", data, cancellationToken: cancellationToken);
//                         }
//                         catch (Exception e)
//                         {
//                             _logger.LogError($"Error while trying to send message ${message}");   
//                         }

                        
//                     }
//                 }, cancellationToken);
//             }

//         }

//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             await DoWork(stoppingToken);
//         }

//         public override Task StartAsync(CancellationToken cancellationToken)
//         {
//             _serial.Open();
//             return base.StartAsync(cancellationToken);
//         }

//         public override Task StopAsync(CancellationToken cancellationToken)
//         {
//             _serial.Close();
//             return base.StopAsync(cancellationToken);
//         }
//     }
}
