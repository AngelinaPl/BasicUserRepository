using System;
using BasicUserRepository.Core.Enums;
using MediatR;

namespace BasicUserRepository.Core.User.v1.UpdateUser
{
    public class UpdateUserRequest : IRequest<UpdateUserResult>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}