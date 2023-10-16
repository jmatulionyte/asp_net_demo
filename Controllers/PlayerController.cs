using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Twest2.Data;
using Twest2.Models;
using Twest2.Helpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Twest2.Controllers
{

    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PlayerController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index(string sortOrder, string searchString)
        {
            var helperPlayer = new HelperPlayer(_db);

            ViewBag.WinsSortParm = sortOrder == "Wins" ? "wins_desc" : "Wins";
            ViewBag.LossesSortParm = sortOrder == "Losses" ? "losses_desc" : "Losses";
            ViewBag.EnrollmentSortParm = sortOrder == "Enrollment" ? "enrollment_desc" : "Enrollment";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

            IEnumerable<Player> objPlayersList = helperPlayer.HandleAllPlayersSorting(sortOrder, searchString);
            return View(objPlayersList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Player obj)
        {
            if (obj.FirstName == obj.LastName.ToString())
            {
                ModelState.AddModelError("FirstName", "LastName cannot match FirstName");
            }
            if (ModelState.IsValid)
            {
                _db.Players.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
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

        //Get
        public IActionResult Delete(int? id)
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

        //DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Players.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Players.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Player deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

