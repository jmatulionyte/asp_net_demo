using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
	public class Tournament
    {
        public Tournament(bool tournamentOngoing)
        {
            TournamentOngoing = tournamentOngoing;
        }

        [Key]
        public int Id { get; set; }

        public bool TournamentOngoing { get; set; } = false;

        public bool PlayoffsOngoing { get; set; } = false;

    }
}

