using AutoMapper;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;
using WebApiWithMappers.Entities.DTOs.ProductDtos;

namespace WebApiWithMappers.Profiles
{
    public class CategoryProfiles:Profile
    {
		public CategoryProfiles()
		{
			CreateMap<Category, GetCategoryDto>().ReverseMap();
			CreateMap<Category, CreateCategoryDto>().ReverseMap();
			CreateMap<Category, UpdateCategoryDto>().ReverseMap();
		}
		
    }
}
