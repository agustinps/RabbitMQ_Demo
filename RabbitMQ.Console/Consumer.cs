using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Al igual que hicimos en el productor, creamos la conexión,
//el canal de comunicación y declaramos la cola a la que suscribirnos.
//Si no existe porque no se ha ejecutado primero el productor, se crea y nos suscribimos igualmente
var factory = new ConnectionFactory { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare("orders", exclusive: false);

    //Utilizamos la clase EventingBasicConsumer del API de RabbitMQ que envía 
    //enventos del ciclo de vida al consumidor, como por ejemplo el de la entrega, que es el que nos interesa en este caso (Received)
    //Cuando se produce este evento ejecutamos el delegado con las acciones necesarias, en este caso, escribir por consola el pedido creado.
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, eventArgs) =>
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(message);
    };
    //Registramos el consumidor para escuchar en la cola indicada.
    channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
    Console.ReadKey();
}
