using AuthStore.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AuthStore.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Messagess messagess)
        {
            await Clients.All.SendAsync("receiveMessage", messagess);
        }
    }
}
