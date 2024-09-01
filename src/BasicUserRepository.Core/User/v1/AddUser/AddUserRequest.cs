using MediatR;
using System;

namespace BasicUserRepository.Core.User.v1.AddUser
{
    public class AddUserRequest : IRequest<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
