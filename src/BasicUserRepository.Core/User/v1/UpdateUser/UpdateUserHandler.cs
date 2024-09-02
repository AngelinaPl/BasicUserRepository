using System.Threading;
using System.Threading.Tasks;
using BasicUserRepository.Core.Enums;
using BasicUserRepository.Core.Services;
using BasicUserRepository.Infrastructure.Models;
using MediatR;

namespace BasicUserRepository.Core.User.v1.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResult>
    {
        private readonly IUserService _service;

        public UpdateUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<UpdateUserResult> Handle(UpdateUserRequest message, CancellationToken token)
        {
            return await _service.UpdateUserAsync(new UpdateUserInfo
            {
                Id = message.Id,
                DateOfBirth = message.DateOfBirth,
                Email = message.Email,
                FirstName = message.FirstName,
                LastName = message.LastName
            }, token);
        }
    }
}