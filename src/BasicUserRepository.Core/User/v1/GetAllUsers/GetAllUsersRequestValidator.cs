using System;
using FluentValidation;

namespace BasicUserRepository.Core.User.v1.GetAllUsers;

public class GetAllUsersRequestValidator : AbstractValidator<GetAllUsersRequest>
{
    public GetAllUsersRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .Must(x => x == null || !string.IsNullOrEmpty(x)).WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must be less than 50 characters.")
            .When(x => x.FirstName != null);

        RuleFor(x => x.LastName)
            .Must(x => x == null || !string.IsNullOrEmpty(x)).WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must be less than 50 characters.")
            .When(x => x.LastName != null);

        RuleFor(x => x.Email)
            .Must(x => x == null || !string.IsNullOrEmpty(x)).WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .When(x => x.Email != null);

        RuleFor(x => x.DateOfBirth)
            .Must(x => x == null || x < DateTime.Now).WithMessage("Date of birth must be in the past.")
            .When(x => x.DateOfBirth != null);
    }
}