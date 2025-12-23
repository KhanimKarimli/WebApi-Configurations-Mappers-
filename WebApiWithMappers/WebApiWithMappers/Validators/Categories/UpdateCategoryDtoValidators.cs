using FluentValidation;
using WebApiWithMappers.Entities.DTOs.CategoryDtos;

namespace WebApiWithMappers.Validators.Categories
{
    public class UpdateCategoryDtoValidators : AbstractValidator<UpdateCategoryDto>
	{
		public UpdateCategoryDtoValidators()
		{
			RuleFor(b => b.Name)
				.NotEmpty().WithMessage("Ad bosluq ola bilmez")
				.NotNull().WithMessage("Ad deyerini daxil edin")
				.MinimumLength(3).WithMessage("Ad en azi 3 simvoldan ibaret olmalidir")
				.MaximumLength(30).WithMessage("Ad en coxu 30 simvoldan ibaret olmalidir");

			RuleFor(b => b.Description)
				.MaximumLength(100).WithMessage("Description en coxu 100 simvoldan ibaret olmalidir");

		}
    }
}
