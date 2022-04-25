using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookTrade.Models
{
    // Model class for User
    public class User
    {
        #region Properties
        // PK for table
        [Key]
        public Guid UserId { get; set; }

        // Max length allowed 50 characters
        [Required]
        [MaxLength(50, ErrorMessage = "FirstName can only be 50 characters")]
        public string FirstName { get; set; }

        // Max length allowed 50 characters
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

        // Max length allowed 100 characters
        [Required]
        [MaxLength(100, ErrorMessage = "Address can only be 100 characters")]
        public string Address { get; set; }
        #endregion
    }
}
