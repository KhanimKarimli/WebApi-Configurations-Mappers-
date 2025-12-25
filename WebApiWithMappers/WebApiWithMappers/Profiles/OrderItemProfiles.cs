using AutoMapper;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;
using WebApiWithMappers.Entities.DTOs.OrderItemDtos;

namespace WebApiWithMappers.Profiles
{
    public class OrderItemProfiles : Profile
	{
		public OrderItemProfiles()
		{
			CreateMap<OrderItem, GetOrderItemDto>();
			CreateMap<CreateOrderItemDto, OrderItem>();
		}
    }
}
