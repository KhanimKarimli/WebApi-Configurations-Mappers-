using FluentValidation;
using WebApiWithMappers.Entities.DTOs.ProductDtos;

namespace WebApiWithMappers.Validators.Products
{
    public class CreateProductDtoValidators : AbstractValidator<CreateProductDto>
	{
		public CreateProductDtoValidators()
		{
			RuleFor(b => b.Name)
				.NotEmpty().WithMessage("Ad bosluq ola bilmez")
				.NotNull().WithMessage("Ad deyerini daxil edin")
				.MinimumLength(3).WithMessage("Ad en azi 3 simvoldan ibaret olmalidir")
				.MaximumLength(50).WithMessage("Ad en coxu 50 simvoldan ibaret olmalidir");

			RuleFor(b => b.Description)
				.MaximumLength(100).WithMessage("Description en coxu 100 simvoldan ibaret olmalidir");

			RuleFor(b => b.Price)
				.NotNull().WithMessage("Qiymeti bos saxlamaq olmaz");

			RuleFor(b => b.Count)
				.NotNull().WithMessage("Say daxil edin");

		}
    }
}
