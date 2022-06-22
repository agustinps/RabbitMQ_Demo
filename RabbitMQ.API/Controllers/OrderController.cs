using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.API.Data;
using RabbitMQ.API.DTOs;
using RabbitMQ.API.Entities;
using RabbitMQ.API.Interfaces;

namespace RabbitMQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMessageProducer messageProducer;
        private readonly IMapper mapper;

        public OrderController(ApplicationDbContext context, IMessageProducer messageProducer, IMapper mapper)
        {
        //Obtenemos por inyección de dependencia una instancia del contexto de la bbdd, de automapper 
        //y de la clase productor que enviará el manesaje a la cola.
            this.context = context;
            this.messageProducer = messageProducer;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderDto orderDto)
        {
            //Creamos el pedido con la orden recibida en el cuerpo de la llamda
            var order = mapper.Map<Order>(orderDto);
            context.Orders.Add(order);
            //Guardamos el pedido en la bbdd (en este caso la bbdd está en memoria)
            await context.SaveChangesAsync();
            //Enviamos el mensaje a la bbdd indicando el pedido creado y la cola en la que guardar el mensaje
            messageProducer.SendMessage(order, "orders");
            //Devolvemos el id del pedido como respuesta a la llamada del api
            return Ok(new { id = order.Id });
        }


    }
}
