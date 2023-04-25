using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
namespace Repository.Repositories
{
   
        public interface IFunctionRepository : IRepository<Function>
        {
            public Task<List<Function>> GetFunctionsOfUser(int AccountId);
        }

        public class FunctionRepository : IFunctionRepository
        {
            private readonly IDbContextFactory<yacht_harbour_ver7Context> _factory;
            public int lastId { get; set; }

        public FunctionRepository(IDbContextFactory<yacht_harbour_ver7Context> factory)
        {
            this._factory = factory;
            using (var context = factory.CreateDbContext())
            {
                this.lastId = context.Functions.ToList().Count + 1;
            }
        }
        public FunctionRepository()
        {
        }

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Function = await context.Functions.FirstOrDefaultAsync(b => b.IdFunction == id);
                if (Function != null)
                {
                    context.Remove(Function);
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<Function>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Functions.ToListAsync();
            }
        }
        public async Task<Function> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Functions.FindAsync(id);
            }
        }
        public async Task<List<Function>> GetFunctionsOfUser(int AccountId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Functions.Where(f => f.AccountIdAccounts.Any(user=>user.IdAccount == AccountId)).ToListAsync();
            }
        }

        public Task<Function> Update(Function entity)
        {
            return null;
        }
        public async Task<Function> Insert(Function entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Functions.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

    }

}
