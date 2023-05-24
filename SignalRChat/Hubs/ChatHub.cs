using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Data;
using SignalRChat.Services;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IHistoryLogger _logger { get; set; }

        public ChatHub(IHistoryLogger logger, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task JoinRoom(string roomName)
        {
            //TODO if room is null
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task GetGroupMessages(string roomName, string senderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

            var room = _dbContext.Rooms.FirstOrDefault(r => r.Name == roomName);

            var messages = await _dbContext.Messages.Where(m => m.ToRoomId == room.Id).ToListAsync();

            // var curUser = _userManager.FindByIdAsync(senderId);
            
            foreach (var message in messages)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceivePrivateMessage", message.SenderIdId, message.Content);
                // await Clients.Client(senderId).SendAsync("ReceivePrivateMessage", message.SenderIdId, message.Content);
                // await Clients.Group(roomName)
                //     .SendAsync("ReceivePrivateMessage", message.SenderIdId, message.Content);
            }
        }

        public async Task SendPrivateMessage(string author, string message, string recieverId, string groupName)
        {
            var room = _dbContext.Rooms.FirstOrDefault(r => r.Name == groupName);
            
            _dbContext.Messages.Add(new Message()
            {
                Content = message,
                Timestamp = DateTime.Today,
                SenderIdId = author,
                RecieverId = recieverId,
                ToRoomId = room.Id
            });
            await _dbContext.SaveChangesAsync();
            await Clients.Group(groupName).SendAsync("ReceivePrivateMessage", author, message);
        }
        
        
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
    
    

    public class MessageDto
    {
        public string Author { get; set; }
        public string Message { get; set; }
        public string ToUser { get; set; }
    }
}