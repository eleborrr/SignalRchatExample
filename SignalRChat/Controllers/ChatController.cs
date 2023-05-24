using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Data;

namespace SignalRChat.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ChatController: Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public ChatController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    [HttpGet("/im")]
    public IActionResult Chats()
    {
        var model = new ChatsModel()
        {
            Chats = _dbContext.Users
        };
        return View(model);
    }
    
    [HttpGet("/im/chat")]
    public async Task<IActionResult> Chat([FromQuery] string id)
    {
        // var roomId = $"{senderId}-{recieverId}";

        var curUserId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;

        var room = _dbContext.Rooms.FirstOrDefault(r => (r.FirstUserId == curUserId || r.FirstUserId == id)
                                               &&
                                               (r.SecondUserId == curUserId || r.SecondUserId == id));
        if (room is null)
        {
            room = new Room()
            {
                FirstUserId = curUserId,
                SecondUserId = id,
                Name = Guid.NewGuid().ToString()
            };
            _dbContext.Rooms.Add(
            room);
            await _dbContext.SaveChangesAsync();
        }
        
        var model = new ChatModel
        {
            UserId = curUserId,
            RecieverId = id,
            RoomName = room.Name
        };
        return View(model);
    }
}