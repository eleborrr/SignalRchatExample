namespace SignalRChat.Data;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public string SenderIdId { get; set; }
    public string RecieverId { get; set; }
    public int ToRoomId { get; set; }
}