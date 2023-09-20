using Iot_Shared.Services;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Device
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DeviceManager _devicemanager;

        public MainWindow(DeviceManager deviceManager)
        {
            InitializeComponent();
            _devicemanager = deviceManager;
            Task.FromResult(SendTelemetryDataAsync());
        }

        private async Task SendTelemetryDataAsync()
        {
            while (true)
            {
                if (_devicemanager.CanSendData)
                {
                    var payload = new
                    {
                        Temp = 22,
                        Humi = 33,
                        Created = DateTime.Now
                    };

                    var json = JsonConvert.SerializeObject(payload);

                    if (await _devicemanager.SendDataAsync(json))
                    CurrentMessageSent.Text = $"Message sent Successfuly:  {json}";
                    await Task.Delay(1000);
                }
            }
        }

    }
}
