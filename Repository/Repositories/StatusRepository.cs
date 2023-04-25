using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public interface IStatusRepository : IRepository<Status>
    {
        public Task<List<Status>> GetYachtStatuses(int id);
        public Task<Status> GetLastYachtStatus(int id);
        public Task<List<Status>> GetUserStatuses(int userId);
        public Task<List<Status>> GetAllSortedStatuses();


    }
    public class StatusRepository : IStatusRepository
    {
        private readonly IDbContextFactory<yacht_harbour_ver7Context> _factory;
        public int lastId { get; set; }

        public StatusRepository(IDbContextFactory<yacht_harbour_ver7Context> factory)
        {
            this._factory = factory;
            using (var context = factory.CreateDbContext())
            {
                this.lastId = context.Statuses.ToList().Count + 1;
            }
        }
        public StatusRepository()
        {
        }

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Status = await context.Statuses.FirstOrDefaultAsync(b => b.IdStatus == id);
                if (Status != null)
                {
                    context.Remove(Status);
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<Status>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Statuses.ToListAsync();
            }
        }
        public async Task<Status> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Statuses.FindAsync(id);
            }
        }
       

        public Task<Status> Update(Status entity)
        {
            return null;
        }
        public async Task<Status> Insert(Status entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Statuses.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }


        public async Task<List<Status>> GetYachtStatuses (int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Statuses.Where(s=> s.YachtIdYacht==id).ToListAsync();
            }

        }
        public async Task<Status> GetLastYachtStatus(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Statuses.Where(s => s.YachtIdYacht == id).OrderByDescending(s=>s.StatusDate).FirstOrDefaultAsync();
            }

        }
        public async Task<List<Status>> GetUserStatuses(int userId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Statuses.Where(s => s.AccountIdAccount == userId).ToListAsync();
            }
        }

        public async Task<List<Status>> GetAllSortedStatuses()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Statuses.OrderByDescending(s => s.StatusDate).ToListAsync();
            }

        }



    }
}
