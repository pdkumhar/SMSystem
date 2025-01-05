using System.ComponentModel.DataAnnotations;

namespace SMSystem.Models
{
    public class Student
    {
        [Key]  // Marking SrNo as the primary key
        public int SrNo { get; set; }

        // Roll Number (Auto-generated)
        public string? RollNumber { get; set; } // Making RollNumber nullable

        // First Name
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No special characters allowed.")]
        public string FirstName { get; set; }

        // Middle Name
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No special characters allowed.")]
        public string MiddleName { get; set; }

        // Last Name
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No special characters allowed.")]
        public string LastName { get; set; }

        // Class (STD)
        [Required]
        [Range(1, 12)]
        public int Class { get; set; }

        // Mobile Number
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public string Mobile { get; set; }

        // Email ID
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        // Address
        [MaxLength(250)]
        public string Address { get; set; }

        // Photograph (Image File Path)
        [Required]
        [RegularExpression(@".*\.(png|jpg|jpeg)$", ErrorMessage = "Only .png, .jpg, and .jpeg file types are allowed.")]
        public string Photograph { get; set; }
    }
}
