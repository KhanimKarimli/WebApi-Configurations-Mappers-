using WebApiWithMappers.Entities.Common;

namespace WebApiWithMappers.Entities
{
    public class Order: BaseEntity
    {
        public string Name { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
