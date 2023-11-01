using System;
using AutoMapper;
using pingPongAPI.Models;
using pingPongAPI.Models.Dto;

namespace pingPongAPI
{
	public class MappingConfig : Profile
	{
        public MappingConfig()
        {
            CreateMap<Player, PlayerDTO>().ReverseMap();
            CreateMap<Player, PlayerCreateDTO>().ReverseMap();
        }
	}
}

