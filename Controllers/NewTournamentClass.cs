using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        // GET: /<controller>/
        public IActionResult Index() //show list of players, 1 item - name+surname, take max 20 users from players list
        {
            //players db - many rows of user info
            //i need to take first and last name properties
            //join them

            IEnumerable<Player> objPlayersList = _db.Players;
            //var newTournament = new NewTournament();
            //var playerList = objPlayersList.Where

            //list of players - item -> name+surname
            IEnumerable<string> listOfPlayers = ((IEnumerable<string>)(from player in objPlayersList select player.FirstName + " " + player.LastName));
            return View(listOfPlayers);
        }

        //public List<List<Player>> GenerateGroups(List<Player> players, int amount)
        //{
        //    Random rnd = new Random();
        //    List
        //    List<NewTournament> groupsForTournament = new List<NewTournament>(); //list to stole lists
        //    for (int i = 0; i < amount; ++i)
        //        groupsForTournament.Add(new Group());

        //    foreach (Team team in teams)
        //    {
        //        int index = rnd.Next(0, amount);
        //        groupsForTournament[index].Add(team);
        //    }
        //    return groupsForTournament;

        //    //return players.OrderBy(item => Guid.NewGuid())
        //    //        .Select((item, index) => new { Item = item, GroupIndex = index % amount })
        //    //        .GroupBy(item => item.GroupIndex,
        //    //                 (key, group) => group.Select(groupItem => groupItem.Item).ToList())
        //    //        .ToList();
        //}
    }
}

