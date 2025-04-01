using Microsoft.AspNetCore.SignalR;

namespace ProductManagementASPNETCoreRazorPage.Hubs
{
    public class SignalrServer : Hub
    {
        public async Task SendReloadSignal()
        {
            await Clients.All.SendAsync("LoadALL");
        }
    }
}
