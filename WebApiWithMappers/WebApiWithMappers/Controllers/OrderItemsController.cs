using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;
using WebApiWithMappers.Entities.DTOs.OrderDtos;
using WebApiWithMappers.Entities.DTOs.OrderItemDtos;
using WebApiWithMappers.Entities.DTOs.ProductDtos;

namespace WebApiWithMappers.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
		private readonly ApiDbContext _context;
		IMapper _mapper;

		public OrderItemsController(ApiDbContext context, IMapper mapper)
		{
			_context=context;
			_mapper=mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllOrderItems()
		{
			var orderItems = await _context.OrderItems.ToListAsync();

			var orderItemDtos = _mapper.Map<List<GetCategoryDto>>(orderItems);

			return Ok(orderItemDtos);
		}

		[HttpGet]
		public async Task<IActionResult> GetOrderItemById(Guid id)
		{
			var orderItem = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);

			if (orderItem == null)
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Sifaris mehsulu tapilmadi"
				});

			var dto = _mapper.Map<GetOrderDto>(orderItem);

			return Ok(dto);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrderItem(CreateOrderItemDto dto)
		{
			{
				var orderitem = _mapper.Map<OrderItem>(dto);
				orderitem.Id=Guid.NewGuid();
				orderitem.CreatedTime = DateTime.UtcNow;
				await _context.OrderItems.AddAsync(orderitem);
				await _context.SaveChangesAsync();
				return NoContent();
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateOrderItem(Guid id, UpdateOrderItemDto dto)
		{
			OrderItem validorderItem = await _context.OrderItems.FirstOrDefaultAsync(t => t.Id == id);
			if (validorderItem == null)
			{
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Sifaris mehsulu tapilmadi"
				});
			}
			validorderItem.PriceTotal = dto.PriceTotal==null ? validorderItem.PriceTotal : dto.PriceTotal;
			validorderItem.Quantity = dto.Quantity==null ? validorderItem.Quantity : dto.Quantity;
			validorderItem.OrderId = dto.OrderId==null ? validorderItem.OrderId : dto.OrderId;
			validorderItem.UpdatedTime = DateTime.UtcNow;
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteOrderItem(Guid id)
		{
			OrderItem validorderitem = await _context.OrderItems.FirstOrDefaultAsync(t => t.Id == id);
			if (validorderitem==null)
			{
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Sifaris mehsulu tapilmadi"
				});
			}
			_context.OrderItems.Remove(validorderitem);
			await _context.SaveChangesAsync();
			return Ok("Deleted");
		}
	}
}
