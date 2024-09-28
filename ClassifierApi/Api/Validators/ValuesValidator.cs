using FluentValidation;

namespace ClassifierApi.Api.Validators
{
    public class ValuesValidator : AbstractValidator<string>
    {
        public ValuesValidator()
        {
            RuleFor(a => a)//.Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(c => c.Length < 20).WithMessage("Specify line");
        }
    }
}
