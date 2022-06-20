using RabbitMQ.API.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.API.Producers
{
    public class RabbitMQProducer : IMessageProducer
    {
        //private readonly IConfiguration configuration;

        //Si queremos obtener la configuración de appsetting para crear el ConnectionFactory
        //public RabbitMQProducer(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}

        public void SendMessage<T>(T message, string routingKey)
        {            
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(routingKey, exclusive: false);
                var jsonObject = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonObject);
                channel.BasicPublish(exchange: "", routingKey: routingKey, body: body);
            }
        }
    }
}
