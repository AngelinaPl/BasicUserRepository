using System.Threading;
using System.Threading.Tasks;
using BasicUserRepository.Core.Models;
using BasicUserRepository.Core.Services;
using MediatR;

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
            return await _service.GetAllUsersAsync(new UserFilter
            {
                DateOfBirth = message.DateOfBirth,
                Email = message.Email,
                FirstName = message.FirstName,
                LastName = message.LastName
            }, token);
        }
    }
}