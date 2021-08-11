namespace RabbitMQDemo.Entities
{
    public interface IMQClient
    {
        void SendMessage(string message);
    }
}