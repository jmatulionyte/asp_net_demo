using Twest2.Data;
using Twest2.Models;

namespace Twest2.Helpers
{
	public class HelperGroup
	{
        private readonly ApplicationDbContext _db;

        public HelperGroup(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<List<string>> SortPlayersToGroups()
		{
            IEnumerable<Player> objPlayersList = _db.Players;
            List<string> GroupA = (from player in objPlayersList where player.Group == "A" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
            List<string> GroupB = (from player in objPlayersList where player.Group == "B" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
            List<string> GroupC = (from player in objPlayersList where player.Group == "C" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
            List<List<string>> groups = new List<List<string>>
            {
                GroupA, GroupB, GroupC
            };
            return groups;
        }
        
        public List<List<string>> CreateSingleGroupPlays(List<string> singleGroup)
        {
            List<List<string>> groupPlays = new List<List<string>>();
            for (var i = 0; i < singleGroup.Count; i++)
            {
                string player = singleGroup[i]; //take player
                List<string> singlePlayInfo = new List<string>(); //list
                for (var j = i+1; j < singleGroup.Count; j++) 
                {
                    string nextPlayer = singleGroup[j];
                    singlePlayInfo.Add(player);
                    singlePlayInfo.Add(nextPlayer);
                    groupPlays.Add(singlePlayInfo);
                    singlePlayInfo = new List<string>(); //nullify list
                }
                //in latter iterations, pair it with all other and add to list

            }
            return groupPlays;
        }

        public void UpdateTournamentDbStartTime()
        {
            DateTime currentDateTime = DateTime.Now;
            var tournamentObj = new Tournament(currentDateTime, new DateTime());
            _db.Tournaments.Add(tournamentObj);
            _db.SaveChanges();
            return;
        }

        public bool checkIfTournamentStarted()
        {
            IEnumerable<Tournament> objTournamentList = _db.Tournaments;
            //check if creation data is not equal to default data and end date is default date -> this means that tournament started but is not yet finished
            bool tournamentStarted = objTournamentList.Any(x => x.TournamentCreationDate != new DateTime() && x.TournamentEndDate == new DateTime());
            return tournamentStarted;

        }
    }
}

