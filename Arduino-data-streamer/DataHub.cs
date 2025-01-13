using Microsoft.AspNetCore.SignalR;

namespace Arduino_data_streamer
{
    public class DataHub : Hub
    {
        public async Task NewMessage(long username, string message) =>
    await Clients.All.SendAsync("messageReceived", username, message);
    }
}
