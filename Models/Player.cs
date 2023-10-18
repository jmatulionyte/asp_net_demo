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

        [Required]
        [RegularExpression(@"^(Yes||No)$", ErrorMessage = "Enrollment value can be either 'Yes' or 'No'")]
        [DisplayName("Enrolled To Tournament")]
        public string EnrolledToTournament { get; set; } = "No";

        [Required]
        [DisplayName("Tournament Wins")]
        public int TournamentWins { get; set; } = 0;

        [Required]
        [DisplayName("Group Wins")]
        public int GroupWins { get; set; } = 0;

        [RegularExpression(@"^(A||B||C)$", ErrorMessage = "Group value can be either 'A', 'B' or 'C'")]
        [DisplayName("Group")]
        public string GroupName { get; set; } = "";
    }
}

