using System.Threading;
using System.Threading.Tasks;
using BasicUserRepository.Core.Models;
using BasicUserRepository.Core.Services;
using MediatR;

namespace BasicUserRepository.Core.User.v1.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserInfo>
    {
        private readonly IUserService _service;

        public GetUserByIdHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<UserInfo> Handle(GetUserByIdRequest message, CancellationToken token)
        {
            return await _service.GetUserByIdAsync(message.Id, token);
        }
    }
}