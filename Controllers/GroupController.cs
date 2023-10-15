using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Twest2.Data;
using Twest2.Controllers;
using Twest2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Twest2.Controllers
{
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _db;

        //private List<List<string>> GroupsABC = new List<List<string>>();

        public GroupController(ApplicationDbContext db)
        {
            _db = db;
            //var _groupViewModel = new GroupViewModel();
        }

        //all players, grup by group name and return 3 tables
        // GET: 
        //public IActionResult Index() //show list of players, 1 item - name+surname, take max 20 users from players list
        //{
        //    var helperClass = new HelperClass(_db);
        //    List<List<string>> groupsABC = helperClass.SortPlayersToGroups();
        //    var _groupViewModel = new GroupViewModel();
        //    _groupViewModel.groupsABC = groupsABC;
        //    return View(_groupViewModel);
        //}

        //all players, grup by group name and return 3 tables + 3 group play tebles
        //GET
        public IActionResult Index(bool createGroupPlays = false)
        {
            var helperClass = new HelperClass(_db);
            List<List<string>> groupsABC = helperClass.SortPlayersToGroups();
            var _groupViewModel = new GroupViewModel();
            _groupViewModel.groupsABC = groupsABC;
            if (createGroupPlays)
            {
                List<List<string>> groupAPlays = helperClass.CreateSingleGroupPlays(groupsABC[0]);
                List<List<string>> groupBPlays = helperClass.CreateSingleGroupPlays(groupsABC[1]);
                List<List<string>> groupCPlays = helperClass.CreateSingleGroupPlays(groupsABC[2]);
                _groupViewModel.groupAPlays = groupAPlays;
                _groupViewModel.groupBPlays = groupBPlays;
                _groupViewModel.groupCPlays = groupCPlays;
            }
            return View(_groupViewModel);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var playerFromDb = _db.Players.Find(id);

            if (playerFromDb == null)
            {
                return NotFound();
            }
            return View(playerFromDb);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Player obj)
        {
            if (ModelState.IsValid)
            {
                _db.Players.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Player edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}

