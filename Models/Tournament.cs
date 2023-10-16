using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
	public class Tournament
    {
        public Tournament(DateTime tournamentCreationDate, DateTime tournamentEndDate, bool tournamentOngoing)
        {
            TournamentCreationDate = tournamentCreationDate;
            TournamentEndDate = tournamentEndDate;
            TournamentOngoing = tournamentOngoing;
        }

        [Key]
        public int Id { get; set; }

        public DateTime TournamentCreationDate { get; set; }

        public DateTime TournamentEndDate { get; set; }

        public bool TournamentOngoing { get; set; } = false;

        public string Place1 { get; set; } = "";

        public string Place2 { get; set; } = "";

        public string Place3 { get; set; } = "";

        public string Place4 { get; set; } = "";

        public string Place5 { get; set; } = "";

        public string Place6 { get; set; } = "";

        public string Place7 { get; set; } = "";

        public string Place8 { get; set; } = "";

    }
}

