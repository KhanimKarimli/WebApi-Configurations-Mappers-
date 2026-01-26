using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.DAL.Repositories.Abstract;
using WebApiWithMappers.DAL.Repositories.Concrete.EfCore;
using WebApiWithMappers.DAL.UnitOfWork.Abstract;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;
using WebApiWithMappers.Entities.DTOs.ProductDtos;

namespace WebApiWithMappers.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
		private readonly IUnitOfWork _unitOfWork;
		IMapper _mapper;
        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
			_unitOfWork=unitOfWork;
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
		[Authorize]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

			var categoryDtos = _mapper.Map<List<GetCategoryDto>>(categories);

			return Ok(categoryDtos);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCategoriesPaginated(int page=1, int size=5)
		{
			var categories = await _unitOfWork.CategoryRepository.GetPaginatedAsync(page,size);

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
			var category = await _unitOfWork.CategoryRepository.Get(x => x.Id == id);

			if (category == null)
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Kateqoriya tapilmadi"
				});

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
				await _unitOfWork.CategoryRepository.AddAsync(category);
				await _unitOfWork.SaveAsync();
				return NoContent();
			}
		}


		[HttpPut]
		public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto dto)
		{
			Category validcategory = await _unitOfWork.CategoryRepository.Get(t => t.Id == id);
			if (validcategory == null)
			{
				return NotFound("Tapilmadi");
			}
			validcategory.Name = dto.Name==null ? validcategory.Name : dto.Name;
			validcategory.Description = dto.Description==null ? validcategory.Description : dto.Description;
			validcategory.UpdatedTime = DateTime.UtcNow;
			_unitOfWork.CategoryRepository.Update(validcategory);
			await _unitOfWork.SaveAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCategory(Guid id)
		{
			Category validcategory = await _unitOfWork.CategoryRepository.Get(t => t.Id == id);
			if (validcategory==null)
			{
				return BadRequest(new
				{
					status = HttpStatusCode.BadRequest,
					message = "Kateqoriya tapilmadi"
				});
			}
			_unitOfWork.CategoryRepository.Delete(validcategory.Id);
			await _unitOfWork.SaveAsync();
			return Ok("Deleted");
		}
	}
}
