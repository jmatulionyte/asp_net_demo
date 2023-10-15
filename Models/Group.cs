using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
	public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Player 1")]
        public string Player1 { get; set; }

        [Required]
        [DisplayName("Player 2")]
        public string Player2 { get; set; }

        [Required]
        [DisplayName("Player 1 Result")]
        public string Player1Result { get; set; }

        [Required]
        [DisplayName("Player 2 Result")]
        public string Player2Result { get; set; }

        public DateTime TournamentCraetionDateTime { get; set; } = DateTime.Now;

    }
}

