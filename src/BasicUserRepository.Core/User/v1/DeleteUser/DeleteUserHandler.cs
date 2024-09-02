using System.Threading;
using System.Threading.Tasks;
using BasicUserRepository.Core.Enums;
using BasicUserRepository.Core.Services;
using MediatR;

namespace BasicUserRepository.Core.User.v1.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResult>
    {
        private readonly IUserService _service;

        public DeleteUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<DeleteUserResult> Handle(DeleteUserRequest message, CancellationToken token)
        {
            return await _service.DeleteUserAsync(message.Id, token);
        }
    }
}