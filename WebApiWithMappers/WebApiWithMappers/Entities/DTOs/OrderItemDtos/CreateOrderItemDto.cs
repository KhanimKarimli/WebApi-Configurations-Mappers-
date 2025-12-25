namespace WebApiWithMappers.Entities.DTOs.OrderItemDtos
{
    public class CreateOrderItemDto
    {
		public int OrderId { get; set; }
		public int Quantity { get; set; }
		public decimal PriceTotal { get; set; }
	}
}
