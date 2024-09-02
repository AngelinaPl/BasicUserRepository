using System;

namespace BasicUserRepository.Infrastructure.Models;

public class UserDBFilter
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
}