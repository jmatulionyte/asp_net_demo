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
                case "Wins":
                    objPlayersList = objPlayersList.OrderBy(s => s.Wins);
                    break;
                case "Losses":
                    objPlayersList = objPlayersList.OrderBy(s => s.Losses);
                    break;
                case "losses_desc":
                    objPlayersList = objPlayersList.OrderByDescending(s => s.Losses);
                    break;
                case "Enrollment":
                    objPlayersList = objPlayersList.OrderBy(s => s.EnrolledToTournament);
                    break;
                case "enrollment_desc":
                    objPlayersList = objPlayersList.OrderByDescending(s => s.EnrolledToTournament);
                    break;
                case "Group":
                    objPlayersList = objPlayersList.OrderBy(s => s.Group);
                    break;
                case "group_desc":
                    objPlayersList = objPlayersList.OrderByDescending(s => s.Group);
                    break;
                default:
                    objPlayersList = objPlayersList.OrderByDescending(s => s.Wins);
                    break;
            }
            return objPlayersList;
        }
    }
}

