using System.ComponentModel.DataAnnotations;

namespace BookTrade.Models
{
    // Model class for Querying Login Details
    public class Login
    {
        #region Properties
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        #endregion
    }
}
