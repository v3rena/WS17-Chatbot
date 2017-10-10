namespace Chatbot.ViewModels
{
    public class MessageViewModel
    {
        public string Message { get; set; }

        public MessageViewModel(string message)
        {
            Message = message;
        }
    }
}