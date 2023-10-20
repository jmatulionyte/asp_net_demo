using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
    public class GroupResult
    {
        public GroupResult(string playerFullName, string groupName, int groupWins)
        {
            PlayerFullName = playerFullName;
            GroupName = groupName;
            GroupWins = groupWins;
        }
        public GroupResult(int positionInGroup)
        {
            PositionInGroup = positionInGroup;
        }

        [Key]
        public int Id { get; set; }

        public string PlayerFullName { get; set; }

        public string GroupName { get; set; }

        public int PositionInGroup { get; set; } = 0;

        public int GroupWins { get; set; }
    }
}