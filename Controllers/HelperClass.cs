using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using NuGet.Packaging;
using Twest2.Data;
using Twest2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Twest2.Controllers
{
	public class HelperClass
	{
        private readonly ApplicationDbContext _db;

        public HelperClass(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<List<string>> SortPlayersToGroups()
		{
            IEnumerable<Player> objPlayersList = _db.Players;
            List<string> GroupA = (from player in objPlayersList where player.Group == "A" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
            List<string> GroupB = (from player in objPlayersList where player.Group == "B" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
            List<string> GroupC = (from player in objPlayersList where player.Group == "C" & player.EnrolledToTournament.ToLower() == "yes" select player.FirstName + " " + player.LastName).ToList();
            List<List<string>> groups = new List<List<string>>
            {
                GroupA, GroupB, GroupC
            };
            return groups;
        }

        public List<List<string>> CreateSingleGroupPlays(List<string> singleGroup)
        {
            List<List<string>> groupPlays = new List<List<string>>();
            for (var i = 0; i < singleGroup.Count; i++)
            {
                string player = singleGroup[i]; //take player
                List<string> singlePlayInfo = new List<string>(); //list
                for (var j = i+1; j < singleGroup.Count; j++) 
                {
                    string nextPlayer = singleGroup[j];
                    singlePlayInfo.Add(player);
                    singlePlayInfo.Add(nextPlayer);
                    groupPlays.Add(singlePlayInfo);
                    singlePlayInfo = new List<string>(); //nullify list
                }
                //in latter iterations, pair it with all other and add to list

            }
            return groupPlays;
        }

        public IEnumerable<Player> HandleAllPlayersSorting(string sortOrder, string searchString)
        {
            IEnumerable<Player> objPlayersList = _db.Players;

            if (!String.IsNullOrEmpty(searchString))
            {
                objPlayersList = objPlayersList.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Wins":
                    objPlayersList = objPlayersList.OrderBy(s => s.Wins);
                    break;
                case "Losses":
                    objPlayersList = objPlayersList.OrderBy(s => s.Losses);
                    break;
                case "losses_desc":
                    objPlayersList = objPlayersList.OrderByDescending(s => s.Losses);
                    break;
                case "Enrollment":
                    objPlayersList = objPlayersList.OrderBy(s => s.EnrolledToTournament);
                    break;
                case "enrollment_desc":
                    objPlayersList = objPlayersList.OrderByDescending(s => s.EnrolledToTournament);
                    break;
                case "Group":
                    objPlayersList = objPlayersList.OrderBy(s => s.Group);
                    break;
                case "group_desc":
                    objPlayersList = objPlayersList.OrderByDescending(s => s.Group);
                    break;
                default:
                    objPlayersList = objPlayersList.OrderByDescending(s => s.Wins);
                    break;
            }
            return objPlayersList;
        }
    }
}

