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
            this.context = context;
            this.messageProducer = messageProducer;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var order = mapper.Map<Order>(orderDto);
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            messageProducer.SendMessage(order, "orders");
            return Ok(new { id = order.Id });
        }


    }
}
