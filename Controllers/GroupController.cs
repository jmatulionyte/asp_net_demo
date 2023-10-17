using Microsoft.AspNetCore.Mvc;
using Twest2.Data;
using Twest2.Models;
using Twest2.Helpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Twest2.Controllers
{
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GroupController(ApplicationDbContext db)
        {
            _db = db;
        }

        //all players, grup by group name and return 3 tables
        //show list of players, 1 item - name+surname, take max 20 users from players list
        //GET
        public IActionResult Index(bool createGroupPlays = false)
        {
            var helperGroup = new HelperGroup(_db);
            List<List<string>> groupsABC = helperGroup.SortPlayersToGroups();
            GroupViewModel groupViewModel = new GroupViewModel();
            groupViewModel.groupsABC = groupsABC;
            bool tournamentStartedAlready = helperGroup.checkIfTournamentOngoing();

            if (createGroupPlays && !tournamentStartedAlready)
            {
                helperGroup.updateGroupTournamentDB(groupsABC);
                groupViewModel.GroupPlaysStarted = true;
                //recheck tournament status if view needs to be updated
                tournamentStartedAlready = helperGroup.checkIfTournamentOngoing();
            }
            if (tournamentStartedAlready)
            {
                //get groupPlays tables
                groupViewModel = helperGroup.sortGroupPlaysByGroupName(groupViewModel);
                //add group plays to database
                //helperGroup.CreateGroupDbPlays(groupViewModel);
            }
            return View(groupViewModel);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var groupPlayObj = _db.Groups.Find(id);

            if (groupPlayObj == null)
            {
                return NotFound();
            }
            return View(groupPlayObj);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Group groupObj)
        {
            if (groupObj.Player1Result == groupObj.Player2Result)
            {
                ModelState.AddModelError("Player1Result", "Player 1 Result cannot match Player 2 Result");
                return View(groupObj);
            }
            HelperGroup helperGroup = new HelperGroup(_db);
            groupObj.Winner = helperGroup.getWinnerFromGroupPlay(groupObj);
            _db.Groups.Update(groupObj);
            _db.SaveChanges();
            TempData["success"] = "Group play edited successfully";
            return RedirectToAction("Index");
        }

    }
}

