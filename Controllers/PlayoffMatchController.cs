using Microsoft.AspNetCore.Mvc;
using Twest2.Data;
using Twest2.Models;
using Twest2.Helpers;

namespace Twest2.Controllers
{
	public class PlayoffMatchController : Controller
	{
        private readonly ApplicationDbContext _db;
        private readonly HelperPlayoffMatch _helperPlayoffMatch;
        private readonly HelperTournament _helperTournament;


        public PlayoffMatchController(ApplicationDbContext db)
        {
            _db = db;
            _helperPlayoffMatch = new HelperPlayoffMatch(_db);
            _helperTournament = new HelperTournament(_db);
        }

        public IActionResult Index()
        {
            bool tournamentStarted = _helperTournament.CheckIfGroupPlaysOngoing();
            //return playoff matches if tournament started
            if (tournamentStarted)
            {
                //get playoff matches from DB
                List<Match> playoffMatches = _helperPlayoffMatch.GetMatchesForPlayoffs();
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
        public IActionResult Edit(Match matchObj)
        {
            HelperMatch _helperMatch = new HelperMatch(_db);
            matchObj.Winner = _helperMatch.GetWinnerFromMatch(matchObj);
            _db.Matches.Update(matchObj);
            _db.SaveChanges();
            _helperPlayoffMatch.GetWinnerOfMatchUpdateMatchDB();
            TempData["success"] = "Playoff match edited successfully";
            return RedirectToAction("Index");
        }
    }
}

