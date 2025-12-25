using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;
using WebApiWithMappers.Entities.DTOs.ProductDtos;

namespace WebApiWithMappers.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		IMapper _mapper;
		public ProductsController(ApiDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		//[HttpGet]

		//public async Task<IActionResult> GetAllProducts()
		//{
		//	var products = await _context.Products.ToListAsync();
		//	return StatusCode((int)HttpStatusCode.OK, products);
		//}


		//[HttpGet]
		//public async Task<ActionResult<List<GetProductDto>>> GetAllProducts()
		//{
		//	var result = await _context.Products.Select(b => new GetProductDto
		//	{
		//		Name = b.Name
		//	}).ToListAsync();

		//	return StatusCode((int)HttpStatusCode.OK, result);
		//}
		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var products = await _context.Products.ToListAsync();

			var productDtos = _mapper.Map<List<GetProductDto>>(products);

			return Ok(productDtos);
		}
		//[HttpGet]

		//public async Task<IActionResult> GetProductById(Guid id)
		//{
		//	Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id==id);
		//	if (product == null)
		//	{
		//		return NotFound();
		//	}
		//	return StatusCode((int)HttpStatusCode.OK, product);
		//}

		[HttpGet]
		public async Task<IActionResult> GetProductById(Guid id)
		{
			var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

			if (product == null)
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Mehsul tapilmadi"
				});

			var dto = _mapper.Map<GetProductDto>(product);

			return Ok(dto);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDto dto)
		{
			{
				var product = _mapper.Map<Product>(dto);
				product.Id=Guid.NewGuid();
				product.CreatedTime = DateTime.UtcNow;
				await _context.Products.AddAsync(product);
				await _context.SaveChangesAsync();
				return NoContent();
			}
		}


		[HttpPut]
		public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto dto)
		{
			Product validproduct = await _context.Products.FirstOrDefaultAsync(t => t.Id == id);
			if (validproduct == null)
			{
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Mehsul tapilmadi"
				});
			}
			validproduct.Name = dto.Name==null ? validproduct.Name : dto.Name;
			validproduct.Description = dto.Description==null ? validproduct.Description : dto.Description;
			validproduct.TotalAmount = dto.Price==null ? validproduct.TotalAmount : dto.Price;
			validproduct.Count = dto.Count==null ? validproduct.Count : dto.Count;
			validproduct.CategoryId = dto.CategoryId==null ? validproduct.CategoryId : dto.CategoryId;
			validproduct.UpdatedTime = DateTime.UtcNow;
			_context.Products.Update(validproduct);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(Guid id)
		{
			Product validproduct = await _context.Products.FirstOrDefaultAsync(t => t.Id == id);
			if (validproduct==null)
			{
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Mehsul tapilmadi"
				});
			}
			_context.Products.Remove(validproduct);
			await _context.SaveChangesAsync();
			return Ok("Deleted");
		}
	}
}
