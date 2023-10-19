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
            _helperPlayoff.CreateUpdatePlayoffData();
            List<GroupResult> groupResultsObj = _db.GroupResults.ToList();
            
            List<KeyValuePair<string, string>> convertedGroupResults = _helperPlayoff.ConvertGroupResultsDataForGraph(groupResultsObj);
            PlayoffGraphData playoffsGraphData = new PlayoffGraphData();
            playoffsGraphData.convertedGroupResults = convertedGroupResults;
            return View(playoffsGraphData);
        }

        public IActionResult EndTournament()
        {
            _helperPlayoff.FinalizeTournamentData();
            return RedirectToAction("Index");
        }
    }
}

