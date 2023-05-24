using Microsoft.AspNetCore.Mvc;
using SignalRChat.Hubs;
using SignalRChat.Services;

namespace SignalRChat.Controllers;

[ApiController]
[Route("[controller]")]
public class HistoryController
{
    private IHistoryLogger _logger;
    
    public HistoryController(IHistoryLogger logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<MessageDto> Get()
    {
        return _logger.GetHistory();
    }
}