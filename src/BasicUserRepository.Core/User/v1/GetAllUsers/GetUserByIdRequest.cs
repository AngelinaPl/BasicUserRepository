using BasicUserRepository.Core.Models;
using MediatR;

namespace BasicUserRepository.Core.User.v1.GetAllUsers
{
    public class GetAllUsersRequest : IRequest<UserInfo[]>
    {
    }
}
