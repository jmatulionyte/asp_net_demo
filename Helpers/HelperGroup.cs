using System.Linq;
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
                    Group groupObj = new Group(player, nextPlayer, groupName);
                    _db.Groups.Add(groupObj);
                    _db.SaveChanges();
                }
            }
        }

      

        /// <summary>
        ///  Loop GroupViewModel model A, B, C lists and add players from list to group DB
        /// </summary>
        /// GroupViewModel
        /// <param name="groupViewModel"> Instantiated GroupViewModel class - helper class for vizualizing all views in griup page
        public void CreateGroupDbPlays(GroupViewModel groupViewModel)
        {
            //loop groups A, B, C lists and add players from list to group DB
            foreach (var play in groupViewModel.groupAPlays)
            {
                //add player1 and player2
                Group groupPlaysObj = new Group(play.Player1, play.Player2, "A");
                _db.Groups.Add(groupPlaysObj);
            }
            foreach (var play in groupViewModel.groupBPlays)
            {
                //add player1 and player2
                Group groupPlaysObj = new Group(play.Player1, play.Player2, "B");
                _db.Groups.Add(groupPlaysObj);
            }
            foreach (var play in groupViewModel.groupCPlays)
            {
                //add player1 and player2
                Group groupPlaysObj = new Group(play.Player1, play.Player2, "C");
                _db.Groups.Add(groupPlaysObj);
            }
            _db.SaveChanges();
        }

        /// <summary>
        ///  Fetch all players from Players DB, sort them into 3 list for A, B, C groups
        ///  Add lists to instance of GroupViewModel class
        /// </summary>
        /// <param name="groupViewModel"> Instantiated GroupViewModel class - helper class for vizualizing all views in griup page
        public GroupViewModel SortGroupPlaysByGroupName(GroupViewModel groupViewModel)
        {
            IEnumerable<Group> objGroupList = _db.Groups;
            List<Group> GroupA = (from sigleMatch in objGroupList
                                  where sigleMatch.GroupName == "A"
                                  select sigleMatch).ToList();

            List<Group> GroupB = (from sigleMatch in objGroupList
                                  where sigleMatch.GroupName == "B"
                                  select sigleMatch).ToList();

            List<Group> GroupC = (from sigleMatch in objGroupList
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
        ///  Checks group match, compares results and returns winner player string
        /// </summary>
        /// <param name="groupObj"> Group class object
        public string getWinnerFromGroupPlay(Group groupObj)
        {
            if (groupObj.Player1Result > groupObj.Player2Result) { return groupObj.Player1; }
            else if (groupObj.Player1Result < groupObj.Player2Result) { return groupObj.Player2; }
            return "NO WINNER";
        }

        /// <summary>
        ///  Checks if need to display group games
        /// </summary>
        /// <param name="groupsABC"> Nested list which hold 3 groups - A, B, C
        /// <param name="createGroupPlays"> indicates if 'start group plays' button clicked
        public GroupViewModel AlignGroupPageView(List<List<string>>  groupsABC, bool createGroupPlays)
        {
            
            HelperTournament _helperT = new HelperTournament(_db);
            //chech if group plays started
            bool tournamentStartedAlready = _helperT.CheckIfGroupPlaysOngoing();
            //check if playoff started
            bool playoffOngoing = _helperT.CheckIfPlayoffOngoing();
            

            GroupViewModel groupViewModel = new GroupViewModel();
            groupViewModel.playoffStarted = playoffOngoing;
            groupViewModel.groupsABC = groupsABC;

            //considering tournament status, show specific types of views in 'group plays' page
            if (createGroupPlays && !tournamentStartedAlready)
            {
                //update tournament status as - already started
                UpdateGroupDBTournamentDB(groupsABC);
                groupViewModel.groupPlaysStarted = true;
                //recheck tournament status if view needs to be updated
                tournamentStartedAlready = _helperT.CheckIfGroupPlaysOngoing();
            }
            if (tournamentStartedAlready)
            {
                //get groupPlays tables
                groupViewModel = SortGroupPlaysByGroupName(groupViewModel);
            }
            return groupViewModel;
        }

        /// <summary>
        ///  Updates GroupResults and Tournaments
        /// </summary>
        public void FinalizeGroupPlaysData()
        {
            SetPlayoffsOngoingTournamentDB();
            UpdateGroupResultsDBWins();
        }

        /// <summary>
        ///  Updates Players table - adds groupWins result to every player
        /// </summary>
        public void UpdateGroupResultsDBWins()
        {
            string[] groups = { "A", "B", "C" };

            //count how many wins for every player in group matches (Group DB) - > playerFullname, totalGroupWins
            List<PlayerWinCount> groupedByWinsCount = _db.Groups
                .Where(t => t.Winner != "")
                .GroupBy(g => g.Winner)
                .Select(w => new PlayerWinCount
                {
                    player = w.Key,
                    winCount = w.Distinct().Count()
                }).ToList();

            if (groupedByWinsCount.Count == 0) ///////////////FIXME
            {
                //throw exception - no results added in groups play!! there should be at least 20 players? so that praph would be filled?
            }

            //loops A, B, C groups
            foreach (var groupName in groups)
            {
                //loop Players DB data (enrolled players to tournament) and set fullName, wins, groupName to groupResult DB
                var playersInGroup = _db.Players.
                    Where(p => p.GroupName == groupName && p.EnrolledToTournament == "Yes").ToList();
                foreach (var player in playersInGroup)
                {
                    string playerFullName = player.FirstName + " " + player.LastName;
                    int groupWins = groupedByWinsCount
                        .Where(x => x.player == playerFullName)
                        .Select(x => x.winCount).SingleOrDefault();
                    GroupResult groupResultsClass = new GroupResult(playerFullName, groupName, groupWins);
                    _db.GroupResults.Add(groupResultsClass);
                }
                _db.SaveChanges();

                //sort groupResults by wins and assign position
                var groupResults = _db.GroupResults.Where(p => p.GroupName == groupName).ToList();
                var groupResultsOrderedByWinsDesc = groupResults.OrderByDescending(t => t.GroupWins).Select(t => t).ToList();
                int positionCounter = 1;
                foreach (var groupResult in groupResultsOrderedByWinsDesc) //error - loop all group, should be only one
                {
                    //groupResult = new GroupResult(positionCounter);
                    groupResult.PositionInGroup = positionCounter;
                    _db.GroupResults.Update(groupResult);
                    positionCounter++;
                }
                _db.SaveChanges();
            }
        }

        /// <summary>
        ///  Get ongoing tournament data and update playoff stage as TRUE
        /// </summary>
        public void SetPlayoffsOngoingTournamentDB()
        {
            var tournamentObj = _db.Tournaments.Where(x => x.GroupPlaysOngoing == true).Select(x => x).SingleOrDefault();
            if (tournamentObj != null)
            {
                tournamentObj.PlayoffsOngoing = true;
                _db.Tournaments.Update(tournamentObj);
                _db.SaveChanges();
            }

        }
    }
}

