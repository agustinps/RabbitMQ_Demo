using AutoMapper;
using RabbitMQ.API.DTOs;
using RabbitMQ.API.Entities;

namespace RabbitMQ.API.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
