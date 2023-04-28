using Microsoft.AspNetCore.SignalR;
using SignalRTesting.Data;
using System.Security.Claims;

namespace SignalRTesting.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        public ChatHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override Task OnConnectedAsync()
        {
            
            Console.WriteLine();
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                var userName = _dbContext.Users.FirstOrDefault(u => u.Id == userId).UserName;

                Clients.Users(HubConnections.OnlineUsers())
                    .SendAsync("ReceiveUserConnected", userId, userName, HubConnections.HasUser(userId));
                HubConnections.AddUserConnection(userId, Context.ConnectionId);              
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (HubConnections.HasUserConnection(userId, Context.ConnectionId))
            {
                var userConnections = HubConnections.Users[userId];
                userConnections.Remove(Context.ConnectionId);

                HubConnections.Users.Remove(userId);

                if (userConnections.Any())
                {
                    HubConnections.Users.Add(userId, userConnections);
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                var userName = _dbContext.Users.FirstOrDefault(u => u.Id == userId).UserName;

                Clients.Users(HubConnections.OnlineUsers())
                    .SendAsync("ReceiveUserDisconnected", userId, userName, HubConnections.HasUser(userId));
                HubConnections.AddUserConnection(userId, Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
