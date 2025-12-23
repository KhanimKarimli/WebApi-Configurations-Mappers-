using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiWithMappers.Entities;

namespace WebApiWithMappers.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{

			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(30);

			builder.Property(p => p.Description)
				.HasMaxLength(100)
				.HasDefaultValue("No description");

		}
	}
}
