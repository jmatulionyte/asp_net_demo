using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
	public class Tournament
    {
        public Tournament(bool groupPlaysOngoing)
        {
            GroupPlaysOngoing = groupPlaysOngoing;
        }

        [Key]
        public int Id { get; set; }

        public bool GroupPlaysOngoing { get; set; } = false;

        public bool PlayoffsOngoing { get; set; } = false;

    }
}

