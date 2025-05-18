using FluentValidation;

namespace CleanArch.Application.Members.Commands.Validators;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().WithMessage("First name is required.")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");
        RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name is required.")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");
        RuleFor(c => c.Gender).NotEmpty().WithMessage("Gender is required.")
            .MinimumLength(4).WithMessage("The gender must be a valid information.");
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .Length(5, 100).WithMessage("Email must be between 5 and 100 characters.");
        RuleFor(c => c.IsActive).NotNull().WithMessage("Is Active is required.");
    }
}