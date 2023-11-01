using System;
using System.Linq.Expressions;
using pingPongAPI.Models;
using pingPongAPI.Repository;
using pingPongAPI.Repository.IRepository;
using static System.Net.Mime.MediaTypeNames;

namespace pingPongAPI.Repository.IRepository
{
    public interface IPlayerRepository : IRepository<Player>
    {
		Task<Player> UpdateAsync(Player entity);
    }
}