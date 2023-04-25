using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<List<Order>> GetUserOrders(int userId);
        

        public Task<List<Order>> GetAllAcceptedOrders();
        public Task<List<Order>> GetYachtOrders(int yachtId);
        public Task<List<Order>> GetYachtsOrders(List<int> yachtIds);
        public Task<Order> UpdateActeptation(int id, bool isAcepted,int boatswainId);
        public Task<List<Order>> GetActeptedOrders(int boatswainId);
        public Task<List<Order>> GetReleasedOrders(int maatId);
        public Task<Order> UpdateRelease(int id, bool isReleased, int maatId);

        public Task<List<Order>> getOrderByTimeAndYacht(int yachtID, DateTime start, DateTime end);

        public Task update(Order order);


        public Task<Order> UpdateReturn(int id, bool isReturned);

        public Task<List<Order>> GetAllUnRealeasedOrders();
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbContextFactory<yacht_harbour_ver7Context> _factory;
        public int lastId { get; set; }

        public OrderRepository(IDbContextFactory<yacht_harbour_ver7Context> factory)
        {
            this._factory = factory;
            using (var context = factory.CreateDbContext())
            {
                lastId = context.Orders.ToList().Count + 1;
            }
        }
        public OrderRepository()
        {
        }
        public async Task Delete(int id)
        {
            
            using (var context = _factory.CreateDbContext())
            {
                
                var Order = await context.Orders.FirstOrDefaultAsync(b => b.IdOrder == id);
                if (Order != null)
                {
                    context.Remove(Order);
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<Order>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Orders.ToListAsync();
            }
        }
        public async Task<Order> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Orders.FindAsync(id);
            }
        }

        public async Task<Order> Insert(Order entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Orders.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

        public async Task update(Order order)
        {
            using (var context = _factory.CreateDbContext())
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
        }
        public async Task<Order> Update(Order order)
        {
            return null;
        }

        public async Task<List<Order>> GetUserOrders(int userId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Orders.Where(o => o.AccountIdAccount == userId).ToListAsync();
            }
        }
       
        public async Task<List<Order>> GetAllAcceptedOrders()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Orders.Where(o => o.IsAccepted == true).ToListAsync();
            }
        }


        public async Task<List<Order>> GetYachtOrders(int yachtId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Orders.Where(o => o.YachtIdYacht == yachtId).ToListAsync();
            }
        }


        public async Task<List<Order>> GetYachtsOrders(List<int> yachtIds)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Orders.Where(o => yachtIds.Contains(o.YachtIdYacht)).OrderBy(o=>o.StartDate).ToListAsync();
            }
        }


        public async Task<Order> UpdateActeptation(int id, bool isAcepted, int boatswainId)
        {
            using (var context = _factory.CreateDbContext())
            {
                var entity = await context.Orders.FirstOrDefaultAsync(o => o.IdOrder == id);
                entity.IsAccepted = isAcepted;
                entity.AccountIdAccount1=boatswainId;
                context.Orders.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<List<Order>> GetActeptedOrders(int boatswainId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Orders.Where(o => o.AccountIdAccount1 == boatswainId).ToListAsync();
            }
        }
        public async Task<List<Order>> GetReleasedOrders(int maatId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Orders.Where(o => o.AccountIdAccount1 == maatId).ToListAsync();
            }
        }
        public async Task<Order> UpdateRelease(int id, bool isReleased, int maatId)
        {
            using (var context = _factory.CreateDbContext())
            {
                var entity = await context.Orders.FirstOrDefaultAsync(o => o.IdOrder == id);
                entity.IsReleased = isReleased;
                entity.AccountIdAccount2 = maatId;
                context.Orders.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<List<Order>> getOrderByTimeAndYacht(int yachtID, DateTime start, DateTime end)
        {
            using (var context = _factory.CreateDbContext())
            {
                var orders = from o in context.Orders
                                   join y in context.Yachts
                                   on o.YachtIdYacht equals yachtID
                                   where (o.StartDate< end
                                   && start < o.EndDate)
                                   select o;
                
                return await Task.FromResult(orders.Distinct().ToList());
            }
        }
        public async Task<Order> UpdateReturn(int id, bool isReturned)
        {
            using (var context = _factory.CreateDbContext())
            {
                var entity = await context.Orders.FirstOrDefaultAsync(o => o.IdOrder == id);
                entity.IsReturned = isReturned;
             
                context.Orders.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<List<Order>> GetAllUnRealeasedOrders()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Orders.Where(o => o.IsReleased == false).ToListAsync();
            }
        }
    }
   
}
