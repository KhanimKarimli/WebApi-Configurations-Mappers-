using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiWithMappers.Entities;

namespace WebApiWithMappers.DAL.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{

			builder.Property(p => p.Quantity)
				.HasDefaultValue(1);
		}
    }
}
