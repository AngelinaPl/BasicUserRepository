using BasicUserRepository.Core.Enums;
using MediatR;

namespace BasicUserRepository.Core.User.v1.DeleteUser
{
    public class DeleteUserRequest : IRequest<DeleteUserResult>
    {
        public int Id { get; set; }
    }
}