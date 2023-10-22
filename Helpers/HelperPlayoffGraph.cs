using System;
using System.Numerics;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.Differencing;
using NuGet.Packaging;
using Twest2.Data;
using Twest2.Models;
using Match = Twest2.Models.Match;

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

        public List<string[]> playoffMatchesTemplate = new List<string[]>
        {
        new string[3]{ "1", "B8", "C1"},
        new string[3]{ "2", "B7", "C2"},
        new string[3]{ "3", "B6", "C3" },
        new string[3]{ "4", "B5", "C4" },

        new string[3]{ "5", "B1", "Winner of 1" },
        new string[3]{ "6", "B2", "Winner of 2" },
        new string[3]{ "7", "B3", "Winner of 3" },
        new string[3]{ "8", "B4", "Winner of 4" },

        new string[3]{ "9", "A8", "Winner of 5" },
        new string[3]{ "10", "A7", "Winner of 6" },
        new string[3]{ "11", "A6", "Winner of 7" },
        new string[3]{ "12", "A5", "Winner of 8" },

        new string[3]{ "13", "A1", "Winner of 9" },
        new string[3]{ "14", "A2", "Winner of 10" },
        new string[3]{ "15", "A3", "Winner of 11" },
        new string[3]{ "16", "A4", "Winner of 12" },

        new string[3]{ "17", "Winner of 13", "Winner of 14" },
        new string[3]{ "18", "Winner of 15", "Winner of 16" },
        new string[3]{ "19", "Winner of 17", "Winner of 18" },
        new string[3]{ "20", "Losser of 17", "Losser of 18" }
        };

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

        /// <summary>
        ///  Create playoff matches and saves to Mathes DB IF they are not already created
        /// </summary>
        public void CreateOrUpdateMatchesForPlayoffs()
        {
            bool playoffMatchesAlreadyInDB = checkIf20PlayoffMatchesCreatedInDB();

            foreach (var match in playoffMatchesTemplate)
            {
                string matchNumber = match[0]; //get match number e.g. 1
                string player1FullName = ValidatePlayersNameForPlayoffs(match[1]);
                string player2FullName = ValidatePlayersNameForPlayoffs(match[2]);
                Match matchesObj = new Match(player1FullName, player2FullName, matchNumber, "Playoff");
                if (!playoffMatchesAlreadyInDB)
                {
                    _db.Matches.Add(matchesObj);
                }
                else
                {
                    Match? specificMatch = _db.Matches.ToList().Where(x => x.GroupName == matchNumber).Single();
                    specificMatch.Player1 = player1FullName;
                    specificMatch.Player2 = player2FullName;
                    _db.Matches.Update(specificMatch);
                }
            }
            _db.SaveChanges();
        }

        /// <summary>
        ///  Create playoff matches and saves to Mathes DB IF they are not already created
        /// </summary>
        public List<Match> GetMatchesForPlayoffs()
        {
            List<Match> playoffMatchesObj = _db.Matches.Where(x => x.MatchType == "Playoff").ToList();
            return playoffMatchesObj;
        }

        /// <summary>
        /// Checks if player name is in positioning list, if not, assigns default positioning (e.g. A7)
        /// </summary>
        private string ValidatePlayersNameForPlayoffs (string playerPositioning)
        {
            string playerPositioningInGroup = playerPositioning;
            string playerGroup = playerPositioning[..1];

            string playerFullName = GetPlayersNameByHisPositioningInGroupDB(playerPositioningInGroup, playerGroup);
            if (playerFullName == null)
            {
                playerFullName = playerPositioningInGroup;
            }
            return playerFullName;
        }

        /// <summary>
        /// Checks if 20 playoff matches is already added to Matches DB
        /// </summary>
        private bool checkIf20PlayoffMatchesCreatedInDB()
        {
            var matchesObj = _db.Matches.ToList();
            bool playoffMatchesAlreadyInDB = matchesObj.Where(x => x.MatchType == "Playoff").Count() == 20;
            return playoffMatchesAlreadyInDB;
        }
        /// <summary>
        ///  Get players name from GroupResults DB by positioning in group data
        /// </summary>
        private string GetPlayersNameByHisPositioningInGroupDB(string playersPositioningData, string groupName)
        {
            string? playerFullName = GetGroupResults()
                .Where(x => playersPositioningData.Contains(x.PositionInGroup.ToString()) == true && x.GroupName == groupName)
                .Select(x => x.PlayerFullName).SingleOrDefault();
            return playerFullName;
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

