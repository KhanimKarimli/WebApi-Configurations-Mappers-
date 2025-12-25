using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;
using WebApiWithMappers.Entities.DTOs.OrderDtos;
using WebApiWithMappers.Entities.DTOs.ProductDtos;

namespace WebApiWithMappers.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApiDbContext _context;
        IMapper _mapper;

        public OrdersController(ApiDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

		[HttpGet]
		public async Task<IActionResult> GetAllOrders()
		{
			var orders = await _context.Orders.ToListAsync();

			var orderDtos = _mapper.Map<List<GetCategoryDto>>(orders);

			return Ok(orderDtos);
		}

		[HttpGet]
		public async Task<IActionResult> GetOrderById(Guid id)
		{
			var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

			if (order == null)
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Sifaris tapilmadi"
				});

			var dto = _mapper.Map<GetOrderDto>(order);

			return Ok(dto);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
		{
			{
				var order = _mapper.Map<Order>(dto);
				order.Id=Guid.NewGuid();
				order.CreatedTime = DateTime.UtcNow;
				await _context.Orders.AddAsync(order);
				await _context.SaveChangesAsync();
				return NoContent();
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderDto dto)
		{
			Order validorder = await _context.Orders.FirstOrDefaultAsync(t => t.Id == id);
			if (validorder == null)
			{
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Sifaris tapilmadi"
				});
			}
			validorder.Name = dto.Name==null ? validorder.Name : dto.Name;
			validorder.TotalAmount = dto.TotalAmount==null ? validorder.TotalAmount : dto.TotalAmount;
			validorder.UpdatedTime = DateTime.UtcNow;
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteOrder(Guid id)
		{
			Order validorder = await _context.Orders.FirstOrDefaultAsync(t => t.Id == id);
			if (validorder==null)
			{
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Sifaris tapilmadi"
				});
			}
			_context.Orders.Remove(validorder);
			await _context.SaveChangesAsync();
			return Ok("Deleted");
		}
	}
}
