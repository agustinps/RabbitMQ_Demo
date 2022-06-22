namespace RabbitMQ.API.Interfaces
{
    public interface IMessageProducer
    {        
        //Interface a implemntar por el productor de mensajes
        void SendMessage<T>(T message, string routingKey);
    }
}
