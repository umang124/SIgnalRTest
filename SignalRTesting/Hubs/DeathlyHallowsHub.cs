using Microsoft.AspNetCore.SignalR;

namespace SignalRTesting.Hubs
{
    public class DeathlyHallowsHub : Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            return SD.DeathlyHallowRace;
        }
    }
}
