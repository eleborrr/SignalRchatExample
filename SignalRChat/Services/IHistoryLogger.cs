using SignalRChat.Hubs;

namespace SignalRChat.Services;

public interface IHistoryLogger
{
    public List<MessageDto> GetHistory();

    public void LogMessage(MessageDto messDto);
}

public class HistoryMessages : IHistoryLogger
{
    private List<MessageDto> _history { get; set; }
    public List<MessageDto> GetHistory() => _history;

    public HistoryMessages()
    {
        _history = new List<MessageDto>();
    }

    public void LogMessage(MessageDto messDto)
    {
        _history.Add(messDto);
    }
}