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

        public PlayoffController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<GroupResult> groupResultsObj = _db.GroupResults.ToList();
            var helperPlayoff = new HelperPlayoff(_db);
            List<KeyValuePair<string, string>> convertedGroupResults = helperPlayoff.ConvertGroupResultsDataForGraph(groupResultsObj);
            PlayoffGraphData playoffsGraphData = new PlayoffGraphData();
            playoffsGraphData.convertedGroupResults = convertedGroupResults;
            return View(playoffsGraphData);
        }
    }
}

