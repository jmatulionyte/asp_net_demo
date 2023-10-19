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
        private readonly HelperGroup _helperGroup;


        public GroupController(ApplicationDbContext db)
        {
            _db = db;
            _helperGroup = new HelperGroup(_db);
        }

        //all players, grup by group name and return 3 tables
        //show list of players, 1 item - name+surname, take max 20 users from players list
        //GET
        public IActionResult Index(bool createGroupPlays = false)
        {
            List<List<string>> groupsABC = _helperGroup.SortPlayersToGroups();
            GroupViewModel groupViewModel = _helperGroup.AlignGroupPageView(groupsABC, createGroupPlays);
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
            groupObj.Winner = _helperGroup.getWinnerFromGroupPlay(groupObj);
            _db.Groups.Update(groupObj);
            _db.SaveChanges();
            TempData["success"] = "Group play edited successfully";
            return RedirectToAction("Index");
        }

       

    }
}

