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
        ///  Gets all players from Players DB
        ///  Returns nested List, which contains Lists for A, B, C groups and players in every group
        /// </summary>
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

        /// <summary>
        ///  Create group plays with 2 players and a group name
        ///  Populate Groups DB with this data for every play
        /// </summary>
        /// <param name="singleGroup"> :List that hold every player in the group
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
        ///  When tournament is started and group plays formed -> add tournament start datetime and bool true to DB
        /// </summary>
        public void UpdateTournamentDBStatusStarted()
        {
                DateTime currentDateTime = DateTime.Now;
                //set start time and bool true, that tournament is ongoing
                Tournament tournamentObj = new Tournament(currentDateTime, new DateTime(), true);
                _db.Tournaments.Add(tournamentObj);
                _db.SaveChanges();
        }

        /// <summary>
        /// Checks if tournament is ongoing and group plays view need to be shown
        /// </summary>
        public bool checkIfTournamentOngoing()
        {
            IEnumerable<Tournament> objTournamentList = _db.Tournaments;
            bool tournamentOngoing = objTournamentList.Any(x => x.TournamentOngoing == true);
            return tournamentOngoing;
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
        public GroupViewModel sortGroupPlaysByGroupName(GroupViewModel groupViewModel)
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
            groupViewModel.GroupPlaysStarted = true;
            return groupViewModel;
        }

        /// <summary>
        ///  Sets created tournament start date and set bool true for ongoingTournament
        ///  Populates Group DB with group Plays for all groups
        /// </summary>
        /// <param name="groupsABC"> Nested list which hold 3 groups - A, B, C
        public void updateGroupTournamentDB(List<List<string>> groupsABC)
        {
            //set tournament creation date in databse
            UpdateTournamentDBStatusStarted();
            //update database with groups a, b, c plays
            CreateSingleGroupPlaysAndUpdateDB(groupsABC[0], "A");
            CreateSingleGroupPlaysAndUpdateDB(groupsABC[1], "B");
            CreateSingleGroupPlaysAndUpdateDB(groupsABC[2], "C");
        }

        /// <summary>
        ///  Sets created tournament start date and set bool true for ongoingTournament
        ///  Populates Group DB with group Plays for all groups
        /// </summary>
        /// <param name="groupsABC"> Nested list which hold 3 groups - A, B, C
        public string getWinnerFromGroupPlay(Group groupObj)
        {
            if (groupObj.Player1Result > groupObj.Player2Result) { return groupObj.Player1; }
            else if (groupObj.Player1Result < groupObj.Player2Result) { return groupObj.Player2; }
            return "NO WINNER";
        }
    }
}

