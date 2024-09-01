using BasicUserRepository.Core.Models;
using BasicUserRepository.Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasicUserRepository.Core.User.v1.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, UserInfo[]>
    {
        private readonly IUserService _service;

        public GetAllUsersHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<UserInfo[]> Handle(GetAllUsersRequest message, CancellationToken token)
        {
            return await _service.GetAllUsersAsync();
        }
    }
}
