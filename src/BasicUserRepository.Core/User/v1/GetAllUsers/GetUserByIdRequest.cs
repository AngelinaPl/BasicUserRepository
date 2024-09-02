using System;
using BasicUserRepository.Core.Models;
using MediatR;

namespace BasicUserRepository.Core.User.v1.GetAllUsers
{
    public class GetAllUsersRequest : IRequest<UserInfo[]>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}