﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRTesting.Data;

namespace SignalRTesting.Hubs
{
    public class BasicChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        public BasicChatHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("MessageReceived", user, message);
        }

        [Authorize]
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            var userId = _dbContext.Users.FirstOrDefault(x => x.Email.ToLower() == receiver.ToLower()).Id;

            if (! string.IsNullOrEmpty(userId))
            {
                await Clients.User(userId).SendAsync("MessageReceived", sender, message);              
            }
        }
    }
}
