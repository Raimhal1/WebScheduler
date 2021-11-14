using FluentValidation;
using WebScheduler.BLL.DtoModels;

namespace WebScheduler.BLL.Validation.ModelValidators
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel> 
    {
        public RegisterUserModelValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().MaximumLength(30);
            RuleFor(user => user.LastName).NotEmpty().MaximumLength(30);
            RuleFor(user => user.UserName).NotEmpty().MaximumLength(30);
            RuleFor(user => user.Email).NotEmpty().MaximumLength(50);
            RuleFor(user => user.Password).NotEmpty().MinimumLength(5);


        }
    }
}
