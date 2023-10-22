using System;
using Twest2.Models;

namespace Twest2.Helpers
{
    public class PlayerViewModel
    {
        public IEnumerable<Player> objPlayersList { get; set; }

        public bool groupPlaysStarted { get; set; }
    }

    public class GroupViewModel
	{
        public List<List<string>> groupsABC { get; set; }

        public List<Match> groupAPlays { get; set; }

        public List<Match> groupBPlays { get; set; }

        public List<Match> groupCPlays { get; set; }

        public bool groupPlaysStarted { get; set; }
    }

    public class PlayerWinCount
    {
        public string player { get; set; }

        public int winCount { get; set; }
    }

    public class PlayoffGraphData
    {
        public List<KeyValuePair<string, string>> convertedGroupResults { get; set; }

        public bool groupPlaysStarted { get; set; }

        public bool playoffStarted { get; set; }

    }

    public class PlayoffMatch
    { //A7, name
        //publi
        public KeyValuePair<string, string> player1Data { get; set; }
        public KeyValuePair<string, string> player2Data { get; set; }
        public int matchNumber { get; set; }
    }

    public class PlayoffMatches
    {

        public List<PlayoffMatch> playoffMatches1_4 { get; set; }
        public List<PlayoffMatch> playoffMatches5_8 { get; set; }
        public List<PlayoffMatch> playoffMatches9_12 { get; set; }
        public List<PlayoffMatch> playoffMatches13_14 { get; set; }
        public List<PlayoffMatch> playoffMatches17_19 { get; set; }
    }
}

