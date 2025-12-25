using WebApiWithMappers.Entities.Common;

namespace WebApiWithMappers.Entities
{
    public class Product: BaseEntity
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public decimal TotalAmount { get; set; }
		public int Count { get; set; }
		public Guid CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
