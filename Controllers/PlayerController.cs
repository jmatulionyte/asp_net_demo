using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twest2.Data;
using Twest2.Models;

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
        public IActionResult Index()
        {
            IEnumerable<Player> objPlayersList = _db.Players;
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

