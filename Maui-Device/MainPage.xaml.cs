using Iot_Shared.Services;
using Newtonsoft.Json;

namespace Maui_Device
{
    public partial class MainPage : ContentPage
    {
       

        private readonly DeviceManager _deviceManager;

        public MainPage(DeviceManager deviceManager)
        {
            InitializeComponent();
            
            _deviceManager = deviceManager;
            Task.FromResult(SendTelemetryDataAsync());
        }
        private async Task SendTelemetryDataAsync()
        {
            while (true)
            {
                if (_deviceManager.CanSendData)
                {
                    var payload = new
                    {
                        Temp = 22,
                        Humi = 33,
                        Created = DateTime.Now
                    };

                    var json = JsonConvert.SerializeObject(payload);

                    if (await _deviceManager.SendDataAsync(json))
                        CurrentMessageSent.Text = $"Message sent Successfuly:  {json}";
                    await Task.Delay(1000);
                }
            }
        }


        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }
}