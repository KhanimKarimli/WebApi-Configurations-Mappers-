using FluentValidation;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;

namespace WebApiWithMappers.Validators.Orders
{
    public class CreateOrderDtoValidators : AbstractValidator<CreateCategoryDto>
	{
		public CreateOrderDtoValidators()
		{
			RuleFor(b => b.Name)
				.NotEmpty().WithMessage("Ad bosluq ola bilmez")
				.NotNull().WithMessage("Ad deyerini daxil edin")
				.MaximumLength(30).WithMessage("Ad en coxu 30 simvoldan ibaret olmalidir");

		}
    }
}
