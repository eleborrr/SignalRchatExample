using Microsoft.AspNetCore.Identity;

namespace SignalRChat.Data;

public class ChatsModel
{
    public IEnumerable<IdentityUser> Chats { get; set; }
}