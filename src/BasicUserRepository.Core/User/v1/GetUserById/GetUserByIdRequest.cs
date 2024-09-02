using BasicUserRepository.Core.Models;
using MediatR;

namespace BasicUserRepository.Core.User.v1.GetUserById;

public class GetUserByIdRequest : IRequest<UserInfo>
{
    public int Id { get; set; }
}