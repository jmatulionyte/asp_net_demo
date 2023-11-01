using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using pingPongAPI.Data;
using pingPongAPI.Models;
using pingPongAPI.Repository.IRepository;

namespace pingPongAPI.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
	{
        private readonly ApplicationDbContext _db;
        //dependency injection
        public PlayerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Player> UpdateAsync(Player entity)
        {
            _db.Players.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

