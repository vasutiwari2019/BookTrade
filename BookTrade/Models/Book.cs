using System;
using System.ComponentModel.DataAnnotations;

namespace BookTrade.Models
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [RegularExpression(@"\b(Mint|Good|Fragile)\b", ErrorMessage = "Condition not allowed")]
        public string Condition { get; set; }

        [Required]
        [RegularExpression(@"\b(Fiction|NonFiction|Comedy|History|Children|Educational|Technical|Others)\b", ErrorMessage = "Genre not allowed")]
        public string Genre { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "About can only be 500 characters")]
        public string About { get; set; }

        public Guid CreatedByUserId { get; set; }

        [Required]
        [RegularExpression(@"\b(Donated|Traded)\b", ErrorMessage = "Trade type not allowed")]
        public string TradeType { get; set; }

        [Required]
        public bool IsTraded { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
