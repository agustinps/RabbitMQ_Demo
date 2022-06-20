# RabbitMQ Demo

### Simple demostración del uso de RabbitMQ mediante una aplicación de consola como consumidor y un API como productor. 

RabbitMQ nos permite crear aplicaciones utilizando colas de mensajes, lo que significa que estas estarán desacopladas y tendrán un mejor rendimiento y escalabilidad. Permite tener productores y suscriptores sin estar al tanto unos de otros y evitar errores o perdida de datos por fallo de alguna parte de nuestro software.

Para ejecutar RabbitMQ tenemos varias opciones, dos de ellas son:

1. Si disponemos de docker, utilizar el siguiente docker run para crear un contenerdor:

          docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

2. Otra opción es crea runa cuenta gratuita en [CloudAMQP](https://www.cloudamqp.com/) que nos permitirá utilizar RabbitMQ.
