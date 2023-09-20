using Iot_Shared.Models;
using Iot_Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_Service
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IHost? Apphost { get; set; }

        public App()
        {
            Apphost = Host.CreateDefaultBuilder().ConfigureServices((config, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton(new IOTHubManager(new IotHubManagerOptions
                {
                    iothubConnectionString = "HostName=Systemutvecklingkaram.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=b1VBnvT3K4uet/1VkdBsuelGyxh6eodhEAIoTJKGAtA=",
                    eventHubEndpoint = "Endpoint=sb://ihsuprodamres076dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=b1VBnvT3K4uet/1VkdBsuelGyxh6eodhEAIoTJKGAtA=;EntityPath=iothub-ehub-systemutve-25230371-5c032015c0",
                    eventHubName = "iothub-ehub-systemutve-25230371-5c032015c0",
                    consumerGroup ="serviceapplikation"
                }));

            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await Apphost!.StartAsync();

            var mainwindow = Apphost.Services.GetRequiredService<MainWindow>();
            mainwindow.Show();

            base.OnStartup(e);

        }
    }
}
