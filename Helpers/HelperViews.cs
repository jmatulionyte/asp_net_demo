using System;
using Twest2.Models;

namespace Twest2.Helpers
{
	public class GroupViewModel
	{
        public List<List<string>> groupsABC { get; set; }

        public List<Group> groupAPlays { get; set; }

        public List<Group> groupBPlays { get; set; }

        public List<Group> groupCPlays { get; set; }

        public bool groupPlaysStarted { get; set; }

        public bool playoffStarted { get; set; }
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
}

