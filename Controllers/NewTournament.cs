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
    public class NewTournamentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NewTournamentController(ApplicationDbContext db)
        {
            _db = db;
        }

        //all players, grup by group name and return 3 tables
        // GET: /<controller>/
        public IActionResult Index() //show list of players, 1 item - name+surname, take max 20 users from players list
        {
            var helperClass = new HelperClass();
            List<List<string>> groupsABC = helperClass.SortPlayersToGroups(_db);
            List<List<string>> groupAPlays = helperClass.CreateSingleGroupPlays(_db, groupsABC[0]);
            List<List<string>> groupBPlays = helperClass.CreateSingleGroupPlays(_db, groupsABC[1]);
            List<List<string>> groupCPlays = helperClass.CreateSingleGroupPlays(_db, groupsABC[2]);
            var model = new TournamentViewModel();
            model.groupsABC = groupsABC;
            model.groupAPlays = groupAPlays;
            model.groupBPlays = groupBPlays;
            model.groupCPlays = groupCPlays;
            return View(model);
        }

        //public IActionResult GroupDropdown()
        //{

        //    List<SelectListItem> items = new List<SelectListItem>();

        //    items.Add(new SelectListItem { Text = "Group A", Value = "0" });

        //    items.Add(new SelectListItem { Text = "Group B", Value = "1" });

        //    items.Add(new SelectListItem { Text = "", Value = "2", Selected = true });

        //    items.Add(new SelectListItem { Text = "Group C", Value = "3" });

        //    ViewBag.Group = items;

        //    return View();

        //}

        //public ViewResult CategoryChosen(string Group)
        //{

        //    ViewBag.messageString = Group;

        //    return View("Information");

        //}
    }
}

