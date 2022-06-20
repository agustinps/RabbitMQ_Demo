﻿namespace RabbitMQ.API.Interfaces
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message, string routingKey);
    }
}
