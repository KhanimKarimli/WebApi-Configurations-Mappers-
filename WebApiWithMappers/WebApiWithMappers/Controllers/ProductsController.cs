using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.DAL.UnitOfWork.Abstract;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;
using WebApiWithMappers.Entities.DTOs.ProductDtos;

namespace WebApiWithMappers.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		IMapper _mapper;
		public ProductsController(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork=unitOfWork;
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
			var products = await _unitOfWork.ProductRepository.GetAllAsync();

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
			var product = await _unitOfWork.ProductRepository.Get(x => x.Id == id);

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
				await _unitOfWork.ProductRepository.AddAsync(product);
				await _unitOfWork.SaveAsync();
				return NoContent();
			}
		}


		[HttpPut]
		public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto dto)
		{
			Product validproduct = await _unitOfWork.ProductRepository.Get(t => t.Id == id);
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
			_unitOfWork.ProductRepository.Update(validproduct);
			await _unitOfWork.SaveAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(Guid id)
		{
			Product validproduct = await _unitOfWork.ProductRepository.Get(t => t.Id == id);
			if (validproduct==null)
			{
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Mehsul tapilmadi"
				});
			}
			_unitOfWork.ProductRepository.Delete(validproduct.Id);
			await _unitOfWork.SaveAsync();
			return Ok("Deleted");
		}
	}
}
