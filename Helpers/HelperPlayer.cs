using Microsoft.AspNetCore.Mvc;
using Twest2.Data;
using Twest2.Models;

namespace Twest2.Helpers
{
	public class HelperPlayer
	{
        private readonly ApplicationDbContext _db;

        public HelperPlayer(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        ///  Helps sort Players table by column, serch row by firstname/lastname
        /// <param name="sortOrder"> string value for which column to order and either descending or ascending order
        /// <param name="searchString"> string value for firstName/Lastname to search for in serch input field
        /// </summary>
        public IEnumerable<Player> HandleAllPlayersSorting(string sortOrder, string searchString)
        {
            IEnumerable<Player> objPlayersList = _db.Players;

            if (!String.IsNullOrEmpty(searchString))
            {
                objPlayersList = objPlayersList.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "TournamentWins":
                    objPlayersList = objPlayersList.OrderBy(s => s.TournamentWins);
                    break;
                case "GroupWins":
                    objPlayersList = objPlayersList.OrderBy(s => s.GroupWins);
                    break;
                case "groupWins_desc":
                    objPlayersList = objPlayersList.OrderByDescending(s => s.GroupWins);
                    break;
                case "Enrollment":
                    objPlayersList = objPlayersList.OrderBy(s => s.EnrolledToTournament);
                    break;
                case "enrollment_desc":
                    objPlayersList = objPlayersList.OrderByDescending(s => s.EnrolledToTournament);
                    break;
                case "Group":
                    objPlayersList = objPlayersList.OrderBy(s => s.GroupName);
                    break;
                case "group_desc":
                    objPlayersList = objPlayersList.OrderByDescending(s => s.GroupName);
                    break;
                default:
                    objPlayersList = objPlayersList.OrderByDescending(s => s.TournamentWins);
                    break;
            }
            return objPlayersList;
        }
    }
}

