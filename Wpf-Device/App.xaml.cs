using Iot_Shared.Services;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_Device
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
                services.AddSingleton(new DeviceManager("HostName=Systemutvecklingkaram.azure-devices.net;DeviceId=WPF-Device;SharedAccessKey=pXXHhXqXDCUg97XbqcmQoDZRpB18VO/VtuMNUyPtaik="));

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
