﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Twest2.Data;
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
            IEnumerable<Player> objPlayersList = _db.Players;
            List<string> GroupA = (from player in objPlayersList where player.Group == "A" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
            List<string> GroupB = (from player in objPlayersList where player.Group == "B" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
            List<string> GroupC = (from player in objPlayersList where player.Group == "C" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
            List<List<string>> groups = new List<List<string>>
            {
                GroupA, GroupB, GroupC
            };
            return View(groups);
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

