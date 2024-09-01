using FluentValidation;

namespace BasicUserRepository.Core.User.v1.GetUserById
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdRequest>
    {
        public GetUserByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
        }
    }
}
