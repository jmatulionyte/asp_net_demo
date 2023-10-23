using Twest2.Data;
using Twest2.Models;

namespace Twest2.Helpers
{
	public class HelperPlayoffGraph
	{
        private readonly ApplicationDbContext _db;

        public HelperPlayoffGraph(ApplicationDbContext db)
        {
            _db = db;
        }

        private List<GroupResult> GetGroupResults()
        {
            return _db.GroupResults.ToList();
        }

        /// <summary>
        ///  Makes list of key value from DB GroupResults, where key - group and position (e.g. A7), value - full name
        /// </summary>
        public List <KeyValuePair<string, string>> ConvertGroupResultsDataForPlayoffs()
        {
            List<GroupResult> groupResultsObj = GetGroupResults();
            
            //class object to store player full name and position (e.g. A7) and use them in graph
            List<KeyValuePair<string, string>> playerPositionGraph = new List<KeyValuePair<string, string>>();
                foreach (var groupResult in groupResultsObj)
            {
                KeyValuePair<string, string> playerResults = new KeyValuePair<string, string>
                (
                    groupResult.GroupName + groupResult.PositionInGroup,
                    groupResult.PlayerFullName
                );
                playerPositionGraph.Add(playerResults);
            }
            return playerPositionGraph;
        }

        public Dictionary<int, Match> ConvertPlayoffMatchesDataForGraph()
        {
            //get playoff matches from DB
            HelperPlayoffMatch _helperPlayoffMatch = new(_db);
            List<Match> playoffMatches = _helperPlayoffMatch.GetMatchesForPlayoffs();

            Dictionary<int, Match> playoffMatchesByMatchNr = new();
            foreach (var match in playoffMatches)
            {
                playoffMatchesByMatchNr[int.Parse(match.GroupName)] = match;
            }
            return playoffMatchesByMatchNr;
        }

        public void GetPlayoffMatchWinnerForGraph()
        {

        }

        public void FinalizeTournamentData()
        {
            //count group wins and according to them set points for players table (ratings)



            //delete group games
            var groupResultsObj = GetGroupResults();
            _db.GroupResults.RemoveRange(groupResultsObj);

            //delete group DB
            var groupObj = _db.Matches;
            _db.Matches.RemoveRange(groupObj);

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

