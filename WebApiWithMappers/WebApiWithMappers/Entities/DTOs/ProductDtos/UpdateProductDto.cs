namespace WebApiWithMappers.Entities.DTOs.ProductDtos
{
    public class UpdateProductDto
    {
		public string Name { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public int Count { get; set; }
		public Guid CategoryId { get; set; }
	}
}
