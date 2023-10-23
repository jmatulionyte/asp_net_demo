using Twest2.Data;
using Twest2.Models;

namespace Twest2.Helpers
{
	public class HelperGroupMatch
	{
        private readonly ApplicationDbContext _db;

        public HelperGroupMatch(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        ///  Returs List of strings with full names of enrolled players from specific group
        /// </summary>
        private List<string> GetListEnrolledPlayersInGroup(IEnumerable<Player> objPlayersList, string groupLetter)
        {
            List<string> groupWithEnrolledPlayers = (from player in objPlayersList
                                   where player.GroupName == groupLetter & player.EnrolledToTournament.ToLower() == "yes"
                                   select player.FirstName + " " + player.LastName).ToList();
            return groupWithEnrolledPlayers;

        }

        /// <summary>
        ///  Gets all players from Players DB
        ///  Returns nested List, which contains Lists for A, B, C groups and players in every group
        /// </summary>
        public List<List<string>> SortPlayersToGroups()
		{
            IEnumerable<Player> objPlayersList = _db.Players;

            List<string> GroupA = GetListEnrolledPlayersInGroup(objPlayersList, "A");
            List<string> GroupB = GetListEnrolledPlayersInGroup(objPlayersList, "B");
            List<string> GroupC = GetListEnrolledPlayersInGroup(objPlayersList, "C");
            List<List<string>> groups = new List<List<string>>
            {
                GroupA, GroupB, GroupC
            };
            return groups;
        }

        /// <summary>
        ///  Create group plays with 2 players and a group name
        ///  Populate Groups DB with this data for every play
        /// </summary>
        /// <param name="singleGroup"> List that hold every player in the group
        /// <param name="groupName"> group name string - A, B, or C
        public void CreateSingleGroupPlaysAndUpdateDB(List<string> singleGroup, string groupName)
        {
            for (var i = 0; i < singleGroup.Count; i++)
            {
                string player = singleGroup[i]; //take player
                for (var j = i + 1; j < singleGroup.Count; j++)//in every iterations, pair player with all other players
                {
                    string nextPlayer = singleGroup[j];
                    Match matchesObj = new Match(player, nextPlayer, groupName, "Group");
                    _db.Matches.Add(matchesObj);
                    _db.SaveChanges();
                }
            }
        }

        /// <summary>
        ///  Fetch all players from Players DB, sort them into 3 list for A, B, C groups
        ///  Add lists to instance of GroupViewModel class
        /// </summary>
        /// <param name="groupViewModel"> Instantiated GroupViewModel class - helper class for vizualizing all views in griup page
        public GroupViewModel SortGroupPlaysByGroupName(GroupViewModel groupViewModel)
        {
            IEnumerable<Match> objMatchesList = _db.Matches;
            List<Match> GroupA = (from sigleMatch in objMatchesList
                                  where sigleMatch.GroupName == "A"
                                  select sigleMatch).ToList();

            List<Match> GroupB = (from sigleMatch in objMatchesList
                                  where sigleMatch.GroupName == "B"
                                  select sigleMatch).ToList();

            List<Match> GroupC = (from sigleMatch in objMatchesList
                                  where sigleMatch.GroupName == "C"
                                  select sigleMatch).ToList();

            groupViewModel.groupAPlays = GroupA;
            groupViewModel.groupBPlays = GroupB;
            groupViewModel.groupCPlays = GroupC;
            groupViewModel.groupPlaysStarted = true;
            return groupViewModel;
        }

        /// <summary>
        ///  Sets created tournament start date and set bool true for ongoingTournament
        ///  Populates Group DB with group Plays for all groups
        /// </summary>
        /// <param name="groupsABC"> Nested list which hold 3 groups - A, B, C
        public void UpdateGroupDBTournamentDB(List<List<string>> groupsABC)
        {
            //set tournament status - group plays ongoing
            HelperTournament _helperT = new HelperTournament(_db);
            _helperT.UpdateTournamentDBStatusStarted();
            //update database with groups a, b, c plays
            CreateSingleGroupPlaysAndUpdateDB(groupsABC[0], "A");
            CreateSingleGroupPlaysAndUpdateDB(groupsABC[1], "B");
            CreateSingleGroupPlaysAndUpdateDB(groupsABC[2], "C");
        }

        /// <summary>
        ///  Checks if need to display group games
        /// </summary>
        /// <param name="groupsABC"> Nested list which hold 3 groups - A, B, C
        /// <param name="createGroupPlays"> indicates if 'start group plays' button clicked
        public GroupViewModel AlignGroupMatchesPageView(List<List<string>>  groupsABC, bool createGroupPlays)
        {
            HelperTournament _helperT = new HelperTournament(_db);
            //chech if group plays started
            bool groupPlaysStarted = _helperT.CheckIfGroupPlaysOngoing();
            
            GroupViewModel groupViewModel = new GroupViewModel();
            groupViewModel.groupsABC = groupsABC;

            //considering tournament status, show specific types of views in 'group plays' page
            if (createGroupPlays && !groupPlaysStarted)
            {
                //update tournament status as - already started
                UpdateGroupDBTournamentDB(groupsABC);
                groupViewModel.groupPlaysStarted = true;
                //recheck tournament status if view needs to be updated
                groupPlaysStarted = _helperT.CheckIfGroupPlaysOngoing();
            }
            if (groupPlaysStarted)
            {
                //get groupPlays tables
                groupViewModel = SortGroupPlaysByGroupName(groupViewModel);
            }
            return groupViewModel;
        }

        /// <summary>
        /// Counts how many wins for every player in group matches (Group DB) - > playerFullname, totalGroupWins
        /// </summary>
        private List<PlayerWinCount> GetPlayersWinsInGroupMatches()
        {
            List<PlayerWinCount> groupedByWinsCount = _db.Matches
                .Where(t => t.Winner != "")
                .GroupBy(g => g.Winner)
                .Select(w => new PlayerWinCount
                {
                    player = w.Key,
                    winCount = w.Distinct().Count()
                }).ToList();
            return groupedByWinsCount;
        }

        /// <summary>
        ///  Updates Players table - adds groupWins result to every player
        /// </summary>
        public void UpdateGroupResultsDBWins()
        {
            string[] groups = { "A", "B", "C" };

            //count how many wins for every player in group matches (Group DB) - > playerFullname, totalGroupWins
            List<PlayerWinCount> groupedByWinsCount = GetPlayersWinsInGroupMatches();

            if (groupedByWinsCount.Count == 0) ///////////////FIXME
            {
                //throw exception - no results added in groups play!! there should be at least 20 players? so that praph would be filled?
            }

            //loops A, B, C groups
            foreach (var groupName in groups)
            {
                //Get Players DB data (enrolled players to tournament and assigned to particullar group (A B C) 
                var playersInGroup = _db.Players.
                    Where(p => p.GroupName == groupName && p.EnrolledToTournament == "Yes").ToList();

                //Loop players DB and and set fullName, wins, groupName to groupResult DB
                foreach (var player in playersInGroup)
                {
                    //set data to groupResults DB
                    SetPlayerWinsToMatchDB(player, groupedByWinsCount, groupName);
                }
                _db.SaveChanges();
                AssignPlaceInGroup(groupName);
            }
        }

        /// <summary>
        /// Check if player is already in GroupResults table and adds his wins score or updates it
        /// </summary>
        private void SetPlayerWinsToMatchDB(Player player, List<PlayerWinCount> groupedByWinsCount, string groupName)
        {
            string playerFullName = player.FirstName + " " + player.LastName;
            //Find how many group matches specific player won
            int groupWins = groupedByWinsCount
                .Where(x => x.player == playerFullName)
                .Select(x => x.winCount).SingleOrDefault();

            GroupResult groupResultsClass;
            
            var groupResultObjSpecificPlayer = _db.GroupResults
                .Where(x => x.PlayerFullName == playerFullName)
                .Select(x => x).ToList();
            //check if player is already in GroupResults DB, if yes - Update his wins sount
            if (groupResultObjSpecificPlayer.Count != 0)
            {
                groupResultObjSpecificPlayer[0].GroupWins = groupWins;
                _db.GroupResults.Update(groupResultObjSpecificPlayer[0]);
            } else //customer is not exitant, so create customer
            {
                groupResultsClass = new GroupResult(playerFullName, groupName, groupWins);
                _db.GroupResults.Add(groupResultsClass);
            }
        }

        /// <summary>
        ///  Get GroupResults data, order desc and assign positions to players
        /// </summary>
        private void AssignPlaceInGroup(string groupName)
        {
            var groupResults = _db.GroupResults.Where(p => p.GroupName == groupName).ToList();
            var groupResultsOrderedByWinsDesc = groupResults.OrderByDescending(t => t.GroupWins).Select(t => t).ToList();
            int positionCounter = 0;
            bool noMatchesPlayedInGroup = groupResultsOrderedByWinsDesc.Where(x => x.GroupWins == 0).Count() == groupResultsOrderedByWinsDesc.Count();
            foreach (var groupResult in groupResultsOrderedByWinsDesc) //error - loop all group, should be only one
            {
                //if after sorting all players in group
                //and expecting biggest score at the top and biggest score - 0
                //means no matches were played in the group
                //setting zero position to all players in group
                if (noMatchesPlayedInGroup) 
                {
                    groupResult.PositionInGroup = 0;
                }
                else { // at least 1 match was played in group, so setting incremental positioning in group
                    positionCounter++;
                    groupResult.PositionInGroup = positionCounter;
                }
               
                _db.GroupResults.Update(groupResult);
            }
            _db.SaveChanges();
        }
    }
}

