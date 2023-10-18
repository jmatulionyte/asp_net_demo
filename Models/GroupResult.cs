using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
    public class GroupResult
    {
        public GroupResult()
        {
        }

        [Key]
        public int Id { get; set; }

        public string PlayerFullName { get; set; }

        public string GroupName { get; set; }

        public int PositionInGroup { get; set; }
    }
}