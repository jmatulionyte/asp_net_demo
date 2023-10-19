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

        /// <summary>
        ///  Get ongoing tournament data and update playoff stage as TRUE
        /// </summary>
        public void SetPlayoffsOngoingTournamentDB()
        {
            var tournamentObj = _db.Tournaments.Where(x => x.TournamentOngoing == true).Select(x => x).SingleOrDefault();
            if (tournamentObj != null)
            {
                tournamentObj.PlayoffsOngoing = true;
                _db.Tournaments.Update(tournamentObj);
                _db.SaveChanges();
            }

        }

        /// <summary>
        ///  Updates Players table - adds groupWins result to every player
        /// </summary>
        public void UpdatePlayersWinsPlayerDB()
        {
            var playerObj = _db.Players.ToList();

            //count how many wins for every player in group matches
            List<PlayerWinCount> groupedByWinsCount = _db.Groups
                .Where(t => t.Winner != "")
                .GroupBy(g => g.Winner)
                .Select(w => new PlayerWinCount
                {
                    player = w.Key,
                    winCount = w.Distinct().Count()
                }).ToList();

            //loop all players in players DB and add group wins data

            foreach (var player in playerObj)
            {
                string playerFullName = player.FirstName + " " + player.LastName;

                int playerWinCount = groupedByWinsCount
                        .Where(x => x.player == playerFullName)
                        .Select(x => x.winCount).FirstOrDefault();
                player.Rating = playerWinCount;
                _db.Players.Update(player);
                _db.SaveChanges();
            }
        }

        /// <summary>
        ///  Create group results data(players, results, groupName) for GroupResults DB 
        /// </summary>
        public void CreateGroupsPositioningUpdateGroupResultDB()
        {
            string[] groups = { "A", "B", "C" };

            foreach (var groupName in groups)
            {//PROBLEM, WHAT IF EQUAL win count??????
                //filter by groupName
                var playersInAGroup = _db.Players.Where(p => p.GroupName == groupName).ToList();
                //sort by biggest win
                var playersInAGroupOrderedByWinsDesc = playersInAGroup.OrderByDescending(t => t.GroupWins);
                int positionCounter = 1;
                foreach (var player in playersInAGroup)
                {
                    GroupResult groupResultsClass = new GroupResult()
                    {
                        PlayerFullName = player.FirstName + " " + player.LastName,
                        PositionInGroup = positionCounter,
                        GroupName = groupName
                    };
                    _db.GroupResults.Add(groupResultsClass);
                    _db.SaveChanges();
                    positionCounter++;
                }
            }
        }

        public void CreateUpdatePlayoffData()
        {
            SetPlayoffsOngoingTournamentDB();
            UpdatePlayersWinsPlayerDB();
            CreateGroupsPositioningUpdateGroupResultDB();
        }

        public void FinalizeTournamentData()
        {
            //count group wins and according to them set points for players table (ratings)



            //delete group games
            var groupResultsObj = _db.GroupResults;
            groupResultsObj.RemoveRange(groupResultsObj);

            //delete group DB
            var groupObj = _db.Groups;
            groupObj.RemoveRange(groupObj);

            //finalize tournament - group plays and playoff are NOT ongoing
            List<Tournament> tournamentsObj = _db.Tournaments.ToList();
            tournamentsObj[0].PlayoffsOngoing = false;
            tournamentsObj[0].TournamentOngoing = false;
            _db.Tournaments.Update(tournamentsObj[0]);


            //delete groupWins in players db column

            //enable buttons where needed

            _db.SaveChanges();


            //update scores in player table - points - rating
        }
    }
}

