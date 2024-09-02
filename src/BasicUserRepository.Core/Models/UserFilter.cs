using System;

namespace BasicUserRepository.Core.Models;

public class UserFilter
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
}