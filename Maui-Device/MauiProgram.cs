using Iot_Shared.Services;
using Microsoft.Extensions.Logging;

namespace Maui_Device
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton(x => new DeviceManager("HostName=Systemutvecklingkaram.azure-devices.net;DeviceId=MAUI-Device;SharedAccessKey=tAex8yAgM1RVr1FiJK1z1h13/fJLcWHO77A9jy5FTIE="));
            builder.Services.AddSingleton<MainPage>();
            //tog pause vid 2 timmar 


//#if DEBUG
//		builder.Logging.AddDebug();
//#endif

            return builder.Build();
        }
    }
}