﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pingPongAPI.Models.Dto
{
    public class PlayerDTO
	{
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EnrolledToTournament { get; set; }

        public int Points { get; set; }

        public string GroupName { get; set; }
    }
}

