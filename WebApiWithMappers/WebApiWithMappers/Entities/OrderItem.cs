using WebApiWithMappers.Entities.Common;

namespace WebApiWithMappers.Entities
{
    public class OrderItem: BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
		public decimal PriceTotal { get; set; }
	}
}
