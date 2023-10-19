using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                case "Rating":
                    objPlayersList = objPlayersList.OrderBy(s => s.Rating);
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
                    objPlayersList = objPlayersList.OrderByDescending(s => s.Rating);
                    break;
            }
            return objPlayersList;
        }

        /// <summary>
        /// Validate that name and surname are not equal and that player is not existant in DB
        /// <param name="playerObj"> Player class object for creation/validation
        /// <param name="ModelState"> ModelStateDictionary for error handling
        /// </summary>
        public void ValidatePlayerCreation(Player playerObj, ModelStateDictionary ModelState)
        {
            if (playerObj.FirstName == playerObj.LastName.ToString())
            {
                ModelState.AddModelError("FirstName", "LastName cannot match FirstName");
            }
            List<Player> playerFromDBWithSameSurname = _db.Players.
                Where(x => x.LastName == playerObj.LastName && x.FirstName == playerObj.FirstName)
                .Select(x => x).ToList();
            if (playerFromDBWithSameSurname.Count() != 0)
            {
               ModelState.AddModelError("FirstName", "This player is already registered with same first name and last name.");
            }
        }
    }
}

