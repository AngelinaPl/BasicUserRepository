﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicUserRepository.Infrastructure.DB.Models
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required] public DateTime DateOfBirth { get; set; }

        [Required] public DateTime CreatedAt { get; set; }

        [Required] public DateTime UpdatedAt { get; set; }
    }
}