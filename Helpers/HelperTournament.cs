using System;
using Twest2.Data;
using Twest2.Models;

namespace Twest2.Helpers
{
	public class HelperTournament
	{
        private readonly ApplicationDbContext _db;

        public HelperTournament(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Checks if tournament is ongoing and group plays view need to be shown
        /// </summary>
        public bool CheckIfGroupPlaysOngoing()
        {
            IEnumerable<Tournament> objTournamentList = _db.Tournaments;
            bool tournamentOngoing = objTournamentList.Any(x => x.GroupPlaysOngoing == true);
            return tournamentOngoing;
        }

        /// <summary>
        /// Checks if playoff is ongoing and playoff view need to be shown
        /// </summary>
        public bool CheckIfPlayoffOngoing()
        {
            IEnumerable<Tournament> objTournamentList = _db.Tournaments;
            //there should be only 1 tournament which is ongoing
            bool playoffOngoing = objTournamentList.Where(x => x.GroupPlaysOngoing == true).Select(x => x.PlayoffsOngoing).SingleOrDefault();
            return playoffOngoing;
        }

        /// <summary>
        ///  When tournament is started and group plays formed -> add tournament ongoing bool = true to DB
        /// </summary>
        public void UpdateTournamentDBStatusStarted()
        {
            //set tournamentOngoing bool true, that tournament is ongoing
            Tournament tournamentObj = new Tournament(true);
            _db.Tournaments.Add(tournamentObj);
            _db.SaveChanges();
        }
    }
}

