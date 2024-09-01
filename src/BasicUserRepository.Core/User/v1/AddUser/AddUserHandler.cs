using MediatR;
using System;
using BasicUserRepository.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace BasicUserRepository.Core.User.v1.AddUser
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, int>
    {
        private readonly IUserService _service;

        public AddUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<int> Handle(AddUserRequest message, CancellationToken token)
        {
            return await _service.AddUserAsync(new Models.UserInfo()
            {
                DateOfBirth = message.DateOfBirth,
                Email = message.Email,
                FirstName = message.FirstName,
                LastName = message.LastName
            });
        }
    }
}
