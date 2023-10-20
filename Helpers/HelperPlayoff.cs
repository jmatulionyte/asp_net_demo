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

        /// <summary>
        ///  Makes list of key value from DB GroupResults, where key - fullname, value - full name
        /// </summary>
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

        public void FinalizeTournamentData()
        {
            //count group wins and according to them set points for players table (ratings)



            //delete group games
            var groupResultsObj = _db.GroupResults;
            _db.GroupResults.RemoveRange(groupResultsObj);

            //delete group DB
            var groupObj = _db.Groups;
            _db.Groups.RemoveRange(groupObj);

            //delete group DB
            var tournamentObj = _db.Tournaments;
            _db.Tournaments.RemoveRange(tournamentObj);

            _db.SaveChanges();

            //delete groupWins in players db column

            //enable buttons where needed




            //update scores in player table - points - rating
        }
    }
}

