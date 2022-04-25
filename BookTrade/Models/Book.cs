using System;
using System.ComponentModel.DataAnnotations;

namespace BookTrade.Models
{
    // Model class for Book
    public class Book
    {
        #region Properties
        // PK for table
        [Key]
        public Guid BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        // Only Mint, Good, Fragile condition valid
        [Required]
        [RegularExpression(@"\b(Mint|Good|Fragile)\b", ErrorMessage = "Condition not allowed")]
        public string Condition { get; set; }

        // Only Fiction, NonFiction, Comedy, History, Children, Educational, Technical, Others valid
        [Required]
        [RegularExpression(@"\b(Fiction|NonFiction|Comedy|History|Children|Educational|Technical|Others)\b", ErrorMessage = "Genre not allowed")]
        public string Genre { get; set; }

        // Max length allowed 500 characters
        [Required]
        [MaxLength(500, ErrorMessage = "About can only be 500 characters")]
        public string About { get; set; }

        public Guid CreatedByUserId { get; set; }

        // Only Donated, Traded valid
        [Required]
        [RegularExpression(@"\b(Donated|Traded)\b", ErrorMessage = "Trade type not allowed")]
        public string TradeType { get; set; }

        [Required]
        public bool IsTraded { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        #endregion

    }
}
