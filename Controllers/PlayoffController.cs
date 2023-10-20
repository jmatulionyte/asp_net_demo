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
    public class PlayoffController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HelperPlayoff _helperPlayoff;

        public PlayoffController(ApplicationDbContext db)
        {
            _db = db;
            _helperPlayoff = new HelperPlayoff(_db);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<GroupResult> groupResultsObj = _db.GroupResults.ToList();
            List<KeyValuePair<string, string>> convertedGroupResults = _helperPlayoff.ConvertGroupResultsDataForGraph(groupResultsObj);

            //get tournament status
            HelperTournament helperT = new HelperTournament(_db);
            bool groupPlaysOngoing = helperT.CheckIfGroupPlaysOngoing();
            bool playoffOngoing = helperT.CheckIfPlayoffOngoing();
            PlayoffGraphData playoffsGraphData = new PlayoffGraphData();

            //assign data to playoff view
            playoffsGraphData.convertedGroupResults = convertedGroupResults;
            playoffsGraphData.groupPlaysStarted = playoffOngoing;
            playoffsGraphData.playoffStarted = playoffOngoing;
            return View(playoffsGraphData);
        }

        public IActionResult EndTournament()
        {
            _helperPlayoff.FinalizeTournamentData();
            return View("Index");
        }
    }
}

