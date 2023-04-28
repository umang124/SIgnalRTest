namespace SignalRTesting.Models.ViewModel
{
    public class ChatVM
    {
        public IList<ChatRoom> Rooms { get; set; }
        public ChatVM()
        {
            Rooms = new List<ChatRoom>();
        }
        public int MaxRoomAllowed { get; set; }
        public string? UserId { get; set; }
        public bool AllowAddRoom => Rooms == null || Rooms.Count < MaxRoomAllowed;
    }
}
