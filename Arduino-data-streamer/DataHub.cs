using Microsoft.AspNetCore.SignalR;

namespace Arduino_data_streamer
{
    public class DataHub : Hub
    {
        private readonly string[] _groups = new string[] { "1", "2", "3" };

        //public override Task OnConnectedAsync()
        //{

        //    var d = Context.Items.Keys;
        //    return base.OnConnectedAsync();
        //}

        public async Task NewMessage(long username, string message) =>
            await Clients.All.SendAsync("messageReceived", username, message);

        public async Task ChangeBot(string botId)
        {
            
            foreach (var botGroup in _groups.Where(x => x != botId)) await Groups.RemoveFromGroupAsync(Context.ConnectionId, botGroup);
            await Groups.AddToGroupAsync(Context.ConnectionId, botId);
            //await Clients.All.SendAsync("test");

        }

    }
}
