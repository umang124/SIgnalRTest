using Microsoft.AspNetCore.SignalR;

namespace SignalRTesting.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;
        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("updatedTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("updatedTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
        public async Task<string> NewWindowLoaded(string name)
        {
            TotalViews++;
            // send update to all clients that total views have been updated
            await Clients.All.SendAsync("updatedTotalViews", TotalViews);
            return "Total Views = " + TotalViews + " from " + name;
        }
    }
}
