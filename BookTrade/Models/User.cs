using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookTrade.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "FirstName can only be 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "LastName can only be 50 characters")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Address can only be 100 characters")]
        public string Address { get; set; }
    }
}
