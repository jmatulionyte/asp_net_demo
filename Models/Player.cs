using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
	public class Player
	{
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Range(1, 1000, ErrorMessage = "Display order must be between 1 and 1000")]

        [Required]
        [RegularExpression("yes||no", ErrorMessage = "Value can be eityher 'Yes' or 'No'")]
        [DisplayName("Enrolled To Tournament")]
        public string EnrolledToTournament { get; set; } = "No";

        [Required]
        [DisplayName("Wins")]
        public int Wins { get; set; } = 0;

        [Required]
        [DisplayName("Losses")]
        public int Losses { get; set; } = 0;

        [DisplayName("Group")]
        public string Group { get; set; } = "";
    }
}

