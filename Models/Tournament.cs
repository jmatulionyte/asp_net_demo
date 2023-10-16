using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
	public class Tournament
    {
        [Key]
        public int Id { get; set; }

        public DateTime TournamentCreationDate { get; set; }

        public DateTime TournamentEndDate { get; set; }

        public int Place1 { get; set; }

        public int Place2 { get; set; }

        public int Place3 { get; set; }

        public int Place4 { get; set; }

        public int Place5 { get; set; }

        public int Place6 { get; set; }

        public int Place7 { get; set; }

        public int Place8 { get; set; }

    }
}

