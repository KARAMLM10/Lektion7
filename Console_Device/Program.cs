using Iot_Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace Console_Device
{
    internal class Program
    {

        public static IHost? Apphost {  get; set; }
        static async Task Main(string[] args)
        {

            Apphost = Host.CreateDefaultBuilder(args)
                .ConfigureServices((config, services) =>
                {
                    services.AddSingleton(new DeviceManager("HostName=Systemutvecklingkaram.azure-devices.net;DeviceId=Console-_Device;SharedAccessKey=0hDB+lutKEcU4hagripeBCv7MCY+I7RztCDOwvso8Y8="));

                }).Build();

            // specifikt för Console applikationer
            using var scop = Apphost.Services.CreateScope();
            var services = scop.ServiceProvider;
            var deviceManager = services.GetRequiredService<DeviceManager>();

            await Apphost.StartAsync();

            Console.Clear();
            Console.WriteLine($"console_device started....");

            await SendTelemtryDataAsync(deviceManager);

            Console.ReadKey();
        }

        private static async Task SendTelemtryDataAsync(DeviceManager deviceManager)
        {
            while (true)
            {
                if (!deviceManager.CanSendData)
                {
                    var payload = new
                    {
                        Temp = 22,
                        Humi =33,
                        Created = DateTime.Now
                    };

                    var json = JsonConvert.SerializeObject(payload);

                    if(await deviceManager.SendDataAsync(json))
                         Console.WriteLine($"Message Sent Successfuly:  {json}");
                               
                    await Task.Delay(1000);
                }
            }
        }
    }
}