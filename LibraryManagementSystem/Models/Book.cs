using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public partial class Book
    {
        [Key]
        [Required]
        public int ID { get; set; }
        
        public string? ProfilePicture { get; set; }
        
        [Required]
        [RegularExpression(@"^(?!0{1,3})\d{3,6}$", ErrorMessage = "BookID must be between 3 and 6 digits and it should not contain one or more zeros.")]
        public int BookID { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])[A-Za-z0-9 ]+$", ErrorMessage = "Please enter a valid name for the book.")]
        public string BookName { get; set; }


        
        [Required]
        [Range(1, 20, ErrorMessage = "BookEdition must be between 1 and 20.")]
        public int BookEdition { get; set; }
        
        [Required]
        [Range(20, int.MaxValue, ErrorMessage = "TotalPages must be at least 20.")]
        public int TotalPages { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Z][a-z]+ [A-Z][a-z]+$", ErrorMessage = "AuthorName must be in the format 'Firstname Lastname'.")]
        public string AuthorName { get; set; }

        
        [Required]
        public DateTime AddedOn { get; set; }
        
        [Required]
        public string Category { get; set; }
    }
}
   