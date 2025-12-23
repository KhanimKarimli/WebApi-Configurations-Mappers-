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
    public class CategoriesController : ControllerBase
    {
		private readonly ApiDbContext _context;
		IMapper _mapper;
		public CategoriesController(ApiDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		//[HttpGet]

		//public async Task<IActionResult> GetAllCategories()
		//{
		//	var categories = await _context.Categories.ToListAsync();
		//	return StatusCode((int)HttpStatusCode.OK, categories);
		//}

		//[HttpGet]
		//public async Task<ActionResult<List<GetCategoryDto>>> GetAllCategories()
		//{
		//	var result = await _context.Categories.Select(b => new GetCategoryDto
		//	{
		//		Name = b.Name
		//	}).ToListAsync();

		//	return StatusCode((int)HttpStatusCode.OK, result);
		//}

		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await _context.Categories.ToListAsync();

			var categoryDtos = _mapper.Map<List<GetCategoryDto>>(categories);

			return Ok(categoryDtos);
		}

		//[HttpGet]

		//public async Task<IActionResult> GetProductById(Guid id)
		//{
		//	Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id==id);
		//	if (category == null)
		//	{
		//		return NotFound();
		//	}
		//	return StatusCode((int)HttpStatusCode.OK, category);
		//}

		[HttpGet]
		public async Task<IActionResult> GetCategoryById(Guid id)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

			if (category == null)
				return NotFound();

			var dto = _mapper.Map<GetCategoryDto>(category);

			return Ok(dto);
		}

		//[HttpPost]
		//public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
		//{
		//	{
		//		Category category = new Category
		//		{
		//			Id=Guid.NewGuid(),
		//			Name = dto.Name,
		//			Description = dto.Description,
		//			CreatedTime = DateTime.UtcNow
		//		};
		//		await _context.Categories.AddAsync(category);
		//		await _context.SaveChangesAsync();
		//		return NoContent();
		//	}
		//}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
		{
			{
				var category = _mapper.Map<Category>(dto);
				category.Id=Guid.NewGuid();
				category.CreatedTime = DateTime.UtcNow;
				await _context.Categories.AddAsync(category);
				await _context.SaveChangesAsync();
				return NoContent();
			}
		}

		//[HttpPut]
		//public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto dto)
		//{
		//	Category validcategory = await _context.Categories.FirstOrDefaultAsync(t => t.Id == id);
		//	if (validcategory == null)
		//	{
		//		return NotFound("Tapilmadi");
		//	}
		//	validcategory.Name = dto.Name==null ? validcategory.Name : dto.Name;
		//	validcategory.Description = dto.Description==null ? validcategory.Description : dto.Description;
		//	validcategory.UpdatedTime = DateTime.UtcNow;
		//	_context.Categories.Update(validcategory);
		//	await _context.SaveChangesAsync();
		//	return Ok();
		//}

		[HttpPut]
		public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto dto)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

			if (category == null)
				return NotFound("Tapılmadı");

			_mapper.Map(dto, category);
			category.UpdatedTime = DateTime.UtcNow;
			await _context.SaveChangesAsync();
			return Ok("Updated");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCategory(Guid id)
		{
			Category validcategory = await _context.Categories.FirstOrDefaultAsync(t => t.Id == id);
			if (validcategory==null)
			{
				return NotFound();
			}
			_context.Categories.Remove(validcategory);
			await _context.SaveChangesAsync();
			return Ok("Deleted");
		}
	}
}
