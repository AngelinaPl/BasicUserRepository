using System.Threading;
using System.Threading.Tasks;
using BasicUserRepository.Core.Models;
using BasicUserRepository.Core.Services;
using MediatR;

namespace BasicUserRepository.Core.User.v1.AddUser;

public class AddUserHandler : IRequestHandler<AddUserRequest, int>
{
    private readonly IUserService _service;

    public AddUserHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<int> Handle(AddUserRequest message, CancellationToken token)
    {
        return await _service.AddUserAsync(new UserInfo
        {
            DateOfBirth = message.DateOfBirth,
            Email = message.Email,
            FirstName = message.FirstName,
            LastName = message.LastName
        }, token);
    }
}