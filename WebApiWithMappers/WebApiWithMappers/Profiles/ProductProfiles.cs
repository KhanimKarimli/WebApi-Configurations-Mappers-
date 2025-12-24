using AutoMapper;
using WebApiWithMappers.Entities;
using WebApiWithMappers.Entities.DTOs.ProductDtos;

namespace WebApiWithMappers.Profiles
{
    public class ProductProfiles:Profile
    {
        public ProductProfiles() 
        {
            CreateMap<CreateProductDto, Product>();
			CreateMap<Product, GetProductDto>();
		}
        
    }
}
