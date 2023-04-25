using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
namespace Repository.Repositories
{
    public interface IYachtRepository : IRepository<Yacht>
    {
        public Task<Yacht> UpdateAvailable(int id, bool isAvailable);

    }
    public class YachtRepository : IYachtRepository
    {
        private readonly IDbContextFactory<yacht_harbour_ver7Context> _factory;
        public int lastId { get; set; }

        public YachtRepository(IDbContextFactory<yacht_harbour_ver7Context> factory)
        {
            this._factory = factory;
            using (var context = factory.CreateDbContext())
            {
                this.lastId = context.Yachts.ToList().Count + 1;
            }
        }
        public YachtRepository()
        {
        }

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Yacht = await context.Yachts.FirstOrDefaultAsync(b => b.IdYacht == id);
                if (Yacht != null)
                {
                    context.Remove(Yacht);
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<Yacht>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Yachts.ToListAsync();
            }
        }
        public async Task<Yacht> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Yachts.FindAsync(id);
            }
        }


        public async Task<Yacht> Update(Yacht updatedEntity)
        {
            using (var context = _factory.CreateDbContext())
            {

                context.Yachts.Update(updatedEntity);
                await context.SaveChangesAsync();
                return updatedEntity;
            }
        }
        public async Task<Yacht> Insert(Yacht entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Yachts.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

        public async Task<Yacht> UpdateAvailable(int id, bool isAvailable)
        {
            using (var context = _factory.CreateDbContext())
            {
                var entity = await context.Yachts.FirstOrDefaultAsync(y => y.IdYacht == id);
                entity.IsAvailable = isAvailable;
               
                context.Yachts.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }





    }
}
