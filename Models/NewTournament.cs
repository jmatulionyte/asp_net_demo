using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twest2.Models
{
	public class NewTournament
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //[DisplayName("First Name")]
        //public string FirstName { get; set; }

        //[Required]
        //[DisplayName("Last Name")]
        //public string LastName { get; set; }
    }
}

