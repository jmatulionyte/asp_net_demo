using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
    public class Group
    {
        public Group(string player1, string player2, string groupName)
        {
            Player1 = player1;
            Player2 = player2;
            GroupName = groupName;
        }

        public Group()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Player 1")]
        public string Player1 { get; set; }

        [Required]
        [DisplayName("Player 2")]
        public string Player2 { get; set; }

        [Range(0, 4, ErrorMessage = "Result can be in range 0-4")]
        [DisplayName("Player 1 Result")]
        public int Player1Result { get; set; } = 0;

        [Range(0, 4, ErrorMessage = "Result can be in range 0-4")]
        [DisplayName("Player 2 Result")]
        public int Player2Result { get; set; } = 0;

        [Required]
        [DisplayName("Group")]
        public string GroupName { get; set; }

        [Required]
        public string Winner { get; set; } = "";
    }
}