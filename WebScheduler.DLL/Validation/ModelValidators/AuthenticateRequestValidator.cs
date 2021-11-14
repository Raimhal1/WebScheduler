using FluentValidation;
using WebScheduler.BLL.DtoModels;

namespace WebScheduler.BLL.Validation.ModelValidators
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest> 
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(request => request.Username).NotEmpty().MaximumLength(50);
            RuleFor(request => request.Password).NotEmpty().MinimumLength(5);
        }
    }

}

