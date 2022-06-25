using RabbitMQ.API.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.API.Producers
{
    public class RabbitMQProducer : IMessageProducer
    {
        //Productor de mensaje para RabbitMQ que implementa el método SendMessage de la interfaz IMessageProducer
        
        //private readonly IConfiguration configuration;

        //Si queremos obtener la configuración de appsetting para crear el ConnectionFactory
        //public RabbitMQProducer(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}

        public void SendMessage<T>(T message, string routingKey)
        {            
            //Creamos la facotria de conexiones en el servidor.
            //Lo ideal sería utilizar IConfiguration para obtener estos datos pero por simplificar 
            //utilizamos localhost de forma estática. No se utiliza usuario y contraseña por lo que 
            //se asignarán los datos por defecto guest - guest.
            var factory = new ConnectionFactory() { HostName = "localhost" };
            //Creamos la conexión.
            var connection = factory.CreateConnection();
            //Creamos el canal de comunicación
            using(var channel = connection.CreateModel())
            {
                //Declaramos la cola en la que publicar el mensaje por el canal creado
                channel.QueueDeclare(routingKey, exclusive: false);
                //Serializamos el mensaje y lo convertimos en byte[] para poder enviarlo
                var jsonObject = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonObject);
                //Publicamos el mensaje en el agente de enrutamiento (exchange) que será el que 
                //envíe el mensaje a la cola correspondiente indicada en routingKey.                 
                channel.BasicPublish(exchange: "", routingKey: routingKey, body: body);
                //Allí quedará a la espera de que un suscriptor consuma este mensaje.
                //Esto lo veremos desde el proyecto de aplicación de consola.
            }
        }
    }
}
