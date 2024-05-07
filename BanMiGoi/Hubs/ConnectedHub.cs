using Microsoft.AspNetCore.SignalR;

namespace ThanhThoaiRestaurant.Hubs
{
    public class ConnectedHub : Hub
    {
        

        public async Task SendMessage(string user, string message)
        {
           
                await Clients.All.SendAsync("ReceiveMessage", user, message);
           
        }

    }
}