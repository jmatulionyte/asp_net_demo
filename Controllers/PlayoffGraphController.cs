using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twest2.Data;
using Twest2.Helpers;
using Twest2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Twest2.Controllers
{
    public class PlayoffGraphController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HelperPlayoffGraph _helperPlayoffGraph;
        private readonly HelperPlayoffMatch _helperPlayoffMatch;

        public PlayoffGraphController(ApplicationDbContext db)
        {
            _db = db;
            _helperPlayoffMatch = new HelperPlayoffMatch(_db);
            _helperPlayoffGraph = new HelperPlayoffGraph(_db);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            Dictionary<int, Match> playoffMatchesForGraph = _helperPlayoffGraph.ConvertPlayoffMatchesDataForGraph();

            //get tournament status
            HelperTournament helperT = new HelperTournament(_db);
            bool groupPlaysOngoing = helperT.CheckIfGroupPlaysOngoing();
            PlayoffGraphData playoffsGraphData = new PlayoffGraphData();

            //assign data to playoff view
            playoffsGraphData.playoffMatchesForGraph = playoffMatchesForGraph;
            playoffsGraphData.groupPlaysStarted = groupPlaysOngoing;

            return View(playoffsGraphData);
        }

        public IActionResult EndTournament()
        {
            _helperPlayoffGraph.FinalizeTournamentData();
            return RedirectToAction("Index");
        }
    }
}

