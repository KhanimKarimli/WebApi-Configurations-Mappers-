using AutoMapper;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.OrderDtos;
using WebApiWithMappers.Entities.DTOs.OrderItemDtos;

namespace WebApiWithMappers.Profiles
{
    public class OrderProfiles : Profile
	{
		public OrderProfiles()
		{
			CreateMap<Order, GetOrderDto>();
			CreateMap<CreateOrderDto, Order>();
		}
    }
}
