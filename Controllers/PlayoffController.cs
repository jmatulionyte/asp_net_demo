using Microsoft.AspNetCore.Mvc;
using Twest2.Data;
using Twest2.Models;
using Twest2.Helpers;

namespace Twest2.Controllers
{
	public class PlayoffController: Controller
	{
        private readonly ApplicationDbContext _db;
        private readonly HelperPlayoffGraph _helperPlayoffGraph;

        public PlayoffController(ApplicationDbContext db)
        {
            _db = db;
            _helperPlayoffGraph = new HelperPlayoffGraph(_db);
        }

        public IActionResult Index()
        {
            bool tournamentStarted = _db.Tournaments.ToList()
                .Select(x => x.GroupPlaysOngoing).SingleOrDefault();
            //return playoff matches if tournament started
            if(tournamentStarted)
            {
                //get playoff matches from DB
                List<Match> playoffMatches = _helperPlayoffGraph.GetMatchesForPlayoffs();
                return View(playoffMatches);
            }
            //If playoff matches not already ctreated and added to DB - create them
            return View();
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var matchFromDB = _db.Matches.Find(id);

            if (matchFromDB == null)
            {
                return NotFound();
            }
            return View(matchFromDB);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Match obj)
        {
            _db.Matches.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Playoff match edited successfully";
            return RedirectToAction("Index");
        }
    }
}

