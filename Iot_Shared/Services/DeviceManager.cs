using Iot_Shared.Models;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Iot_Shared.Services;

public class DeviceManager
{
    private DeviceClient _deviceclient = null!;
    private string _connectionString = null!;

    public bool CanSendData {  get; private set; } = true;

    public DeviceManager(string deviceConnectioString )
    {
        _connectionString = deviceConnectioString;
        _deviceclient = DeviceClient.CreateFromConnectionString(_connectionString);
        Task.FromResult( _deviceclient.SetMethodDefaultHandlerAsync(MethodDefaultCallback, null) );
    }

    //public virtual async Task StartAsync()
    //{
    //    await _deviceclient.SetMethodDefaultHandlerAsync(MethodDefaultCallback, null); 

    //}

    private async Task<MethodResponse> MethodDefaultCallback(MethodRequest req, object context)
    {

        var res = new MethodDataResponse();
        try
        {

            res.Message = $"Method {req.Name} excuted Successfuly.";
            
            switch (req.Name.ToLower())
            {
                case "start":
                    CanSendData = true;
                    break;
                case "stop":
                    CanSendData = false;
                    break;
                default:
                    res.Message = $" Method {req.Name} could be not Found. ";
                    return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 404);

            }
            await Task.Delay(10);
            return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 200);
        }
        catch (Exception ex)
        {
            res.Message = $" Error : {ex.Message}";
            return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 400);
        }
    }

    public async Task<bool> SendDataAsync(string Payload) 
    {
        try
        {
            var message = new Message(Encoding.UTF8.GetBytes(Payload));
            await _deviceclient.SendEventAsync(message);
            await Task.Delay(10);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }


    

}



