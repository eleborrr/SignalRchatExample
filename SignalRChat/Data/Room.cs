namespace SignalRChat.Data;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FirstUserId { get; set; }
    public string SecondUserId { get; set; }
    public ICollection<Message> Messages { get; set; }
}