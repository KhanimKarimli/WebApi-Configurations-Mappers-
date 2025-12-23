using AutoMapper;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.ProductDtos;

namespace WebApiWithMappers.Profiles
{
    public class ProductProfiles:Profile
    {
        public ProductProfiles() 
        {
            CreateMap<Product, CreateProductDto>().ReverseMap();
			CreateMap<Product, GetProductDto>();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
		}
        
    }
}
