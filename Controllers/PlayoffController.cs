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

            //List<PlayoffMatch> allMatch = _helperPlayoffGraph.CreateMatchesForPlayoffs1_4();
            //check if already craeted 20 playoff matches
            _helperPlayoffGraph.CreateMatchesForPlayoffs();
            //this.User -> saved during login operation
            //return View(allMatch);
            return View();

        }
    }
}

