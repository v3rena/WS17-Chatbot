namespace Chatbot.Interfaces.DTOs
{
    public interface IMessage
    {
        string Content { get; set; }
        string Guid { get; set; }
    }
}