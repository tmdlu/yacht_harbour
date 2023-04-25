using DataBaseAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Repository.Repositories
{

    public interface IAccountRepository : IRepository<Account>
    {
        public Task<Account> UpdateStatus(int id, string newStatus);
        public Task<List<Account>> GetLowerAccounts();
        public Task<List<Account>> GetAccountsWithoutAdmin();
        public Task<List<Account>> GetRolelessAccounts();


    }

    public class AccountRepository : IAccountRepository
    {
        private readonly IDbContextFactory<yacht_harbour_ver7Context> _factory;
        public int lastId { get; set; }
        public AccountRepository(IDbContextFactory<yacht_harbour_ver7Context> factory)
        {
            this._factory = factory;
        }
        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Account = await context.Accounts.FirstOrDefaultAsync(b => b.IdAccount == id);
                if (Account != null)
                {
                    context.Remove(Account);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Account>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Accounts.ToListAsync();
            }
        }

        public async Task<Account> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Accounts.FindAsync(id);
            }
        }


        public async Task<Account> Insert(Account entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Accounts.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

        public async Task<Account> Update(Account updatedEntity)
        {
            using (var context = _factory.CreateDbContext())
            {

                context.Accounts.Update(updatedEntity);
                await context.SaveChangesAsync();
                return updatedEntity;
            }
        }

        public async Task<Account> UpdateStatus(int id, string newStatus)
        {
            using (var context = _factory.CreateDbContext())
            {
                var entity = await context.Accounts.FirstOrDefaultAsync(b => b.IdAccount == id);
                entity.ClubStatus = newStatus;
                context.Accounts.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<List<Account>> GetLowerAccounts()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Accounts.Where(a => a.Role != "Boatswain" && a.Role != "Admin").ToListAsync();
            }
        }

        public async Task<List<Account>> GetAccountsWithoutAdmin()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Accounts.Where(a => a.Role != "Admin").ToListAsync();
            }
        }

        public async Task<List<Account>> GetRolelessAccounts()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Accounts.Where(a => a.Role!= "Boatswain" && a.Role != "Admin" && a.Role!="Maat" && a.Role != "Sailor").ToListAsync();
            }
        }
    }
}
