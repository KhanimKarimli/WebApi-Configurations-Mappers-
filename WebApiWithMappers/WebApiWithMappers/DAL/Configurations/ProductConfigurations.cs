using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiWithMappers.Entities;

namespace WebApiWithMappers.DAL.Configurations
{
	public class ProductConfigurations : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{

			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(p => p.Description)
				.HasMaxLength(100)
				.HasDefaultValue("No description");

			builder.Property(p => p.Price)
				.IsRequired();

			builder.Property(p => p.Count)
				.HasDefaultValue(1);
		}
	}
}
