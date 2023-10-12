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
        [DisplayName("Enrolled To Tournament")]
        public bool EnrolledToTournament { get; set; } = false;

        [Required]
        [DisplayName("Wins")]
        public int Wins { get; set; } = 0;

        [Required]
        [DisplayName("Losses")]
        public int Losses { get; set; } = 0;
    }
}

