using FluentValidation;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;

namespace WebApiWithMappers.Validators.OrderItems
{
    public class UpdateOrderItemValidators : AbstractValidator<CreateCategoryDto>
	{
		public UpdateOrderItemValidators()
		{
			RuleFor(b => b.Name)
				.NotEmpty().WithMessage("Ad bosluq ola bilmez")
				.NotNull().WithMessage("Ad deyerini daxil edin")
				.MaximumLength(30).WithMessage("Ad en coxu 30 simvoldan ibaret olmalidir");
		}
	}
}
