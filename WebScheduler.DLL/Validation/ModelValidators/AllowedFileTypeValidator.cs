using FluentValidation;
using WebScheduler.BLL.DtoModels;

namespace WebScheduler.BLL.Validation.ModelValidators
{
    public class AllowedFileTypeValidator : AbstractValidator<AllowedFileTypeDto>
    {
        public AllowedFileTypeValidator()
        {
            RuleFor(request => request.FileType).NotEmpty().MaximumLength(8);
            RuleFor(request => request.FileSize).NotEmpty().NotNull();
        }
    }
}
