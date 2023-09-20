
namespace Iot_Shared.Models;

public class IotHubManagerOptions
{
    public string iothubConnectionString { get; set; } = null;
    public string eventHubEndpoint { get; set; } = null;
    public string eventHubName { get; set; } = null;
    public string consumerGroup { get; set; } = null;
}
