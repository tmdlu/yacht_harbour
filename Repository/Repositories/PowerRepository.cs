using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public interface IPowerRepository : IRepository<Power>
    {
        public Task<List<Power>> GetPowersOfUser(int AccountId);
    }
    public class PowerRepository : IPowerRepository
    {
        private readonly IDbContextFactory<yacht_harbour_ver7Context> _factory;
        public int lastId { get; set; }

        public PowerRepository(IDbContextFactory<yacht_harbour_ver7Context> factory)
        {
            this._factory = factory;
            using (var context = factory.CreateDbContext())
            {
                this.lastId = context.Powers.ToList().Count + 1;
            }
        }
        public PowerRepository()
        {
        }
        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Power = await context.Powers.FirstOrDefaultAsync(b => b.IdPower == id);
                if (Power != null)
                {
                    context.Remove(Power);
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<Power>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Powers.ToListAsync();
            }
        }
        public async Task<Power> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Powers.FindAsync(id);
            }
        }
        public Task<Power> Update(Power entity)
        {
            return null;
        }

        public async Task<Power> Insert(Power entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Powers.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

        public async Task<List<Power>> GetPowersOfUser(int AccountId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Powers.Where(p => p.AccountIdAccounts.Any( user=>user.IdAccount == AccountId)).ToListAsync();
            }
        }
        


    }







    
}
