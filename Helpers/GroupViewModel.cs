﻿using System;
using Twest2.Models;

namespace Twest2.Helpers
{
	public class GroupViewModel
	{
        public List<List<string>> groupsABC { get; set; }

        public List<Group> groupAPlays { get; set; }

        public List<Group> groupBPlays { get; set; }

        public List<Group> groupCPlays { get; set; }

        public bool GroupPlaysStarted { get; set; }
    }
}
