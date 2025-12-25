using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiWithMappers.Entities;

namespace WebApiWithMappers.DAL.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{

			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(30);
		}
    }
}
