

using FluentValidation;

namespace DominicanWhoCodes.Identity.API.Models.Validators
{
    public class NewUserValidator : AbstractValidator<User>
    {
        public NewUserValidator()
        {
            RuleFor(e => e.FirstName)
                .NotNull()
                .WithMessage("The first name field is required.")
                .Length(1, 80)
                .WithMessage("The first name must be between 1 and 80 character.");

            RuleFor(e => e.LastName)
                .NotNull()
                .WithMessage("The last name field is required.")
                .Length(1, 250)
                .WithMessage("the last name must be between 1 and 250 character.");

            RuleFor(e => e.Email)
                .NotNull()
                .WithMessage("The email field is required.")
                .Length(1, 256)
                .WithMessage("The email must be between 1 and 256 character.")
                .EmailAddress()
                .WithMessage("The email is not valid.");
        }
    }
}
