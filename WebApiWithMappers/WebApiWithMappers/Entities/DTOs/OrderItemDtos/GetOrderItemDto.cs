namespace WebApiWithMappers.Entities.DTOs.OrderItemDtos
{
    public class GetOrderItemDto
    {
		public int OrderId { get; set; }
		public int Quantity { get; set; }
		public decimal PriceTotal { get; set; }
	}
}
