using System;
using System.Text.RegularExpressions;
using Twest2.Data;
using Twest2.Models;

namespace Twest2.Helpers
{
	public class HelperPlayoff
	{
        private readonly ApplicationDbContext _db;

        public HelperPlayoff(ApplicationDbContext db)
        {
            _db = db;
        }

        //make list or key value, where A1 - full name
        public List <KeyValuePair<string, string>> ConvertGroupResultsDataForGraph(List<GroupResult> groupResultsObj)
        {
            List<KeyValuePair<string, string>> groupResults = new List<KeyValuePair<string, string>>();
            foreach (var playerResult in groupResultsObj)
            {
                KeyValuePair<string, string> playerResults = new KeyValuePair<string, string>
                (
                    playerResult.GroupName + playerResult.PositionInGroup,
                    playerResult.PlayerFullName
                );
                groupResults.Add(playerResults);
            }
            return groupResults;

        }


        //IEnumerable<Player> objPlayersList = _db.Players;
        //List<string> GroupA = (from player in objPlayersList where player.GroupName == "A" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
        //List<string> GroupB = (from player in objPlayersList where player.GroupName == "B" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
        //List<string> GroupC = (from player in objPlayersList where player.GroupName == "C" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
        //List<List<string>> groups = new List<List<string>>
        //    {
        //        GroupA, GroupB, GroupC
        //    };
        //    return groups;
	}
}

