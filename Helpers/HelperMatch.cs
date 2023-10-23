using Twest2.Data;
using Twest2.Models;

namespace Twest2.Helpers
{
	public class HelperMatch
	{
        private readonly ApplicationDbContext _db;

        public HelperMatch(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        ///  Checks group match, compares results and returns winner player string
        /// </summary>
        /// <param name="matchObj"> Match class object
        public string GetWinnerFromMatch(Match matchObj)
        {
            if (matchObj.Player1Result > matchObj.Player2Result) { return matchObj.Player1; }
            else if (matchObj.Player1Result < matchObj.Player2Result) { return matchObj.Player2; }
            return "NO WINNER";
        }
    }
}

