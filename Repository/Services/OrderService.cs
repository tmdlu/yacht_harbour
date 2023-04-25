using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;
namespace Repository.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository; 
        }

        public  Task<List<Order>> GetAllOrders()
        {
            return  _orderRepository.GetAll();
        }
        public async Task<bool> updateRelease(int orderId, bool newStatus, int maatId)
        {
            await _orderRepository.UpdateRelease(orderId, newStatus, maatId);
            return newStatus;
        }
        public async Task<bool> updateActeptation(int orderId, bool newStatus, int boatswainId)
        {
            await _orderRepository.UpdateActeptation(orderId, newStatus, boatswainId);
            return newStatus;
        }

        public async Task deleteOrderByID(int id)
        {
            await _orderRepository.Delete(id);
        }
        public Task<List<Order>> GetUserOrders(int id)
        {
            return _orderRepository.GetUserOrders(id);
        }


        public Task<List<Order>> GetYachtOrders(int id)
        {
            return _orderRepository.GetYachtOrders(id);
        }
        public Task<List<Order>> GetYachtsOrders(List<int> ids)
        {
            return _orderRepository.GetYachtsOrders(ids);
        }

        public async Task<Order> GetOrderByID(int id)
        {
            return await _orderRepository.GetById(id);
        }
        public async Task<Order> addNewOrder(Order order)
        {
            return await _orderRepository.Insert(order);
        }

        public async Task updateOrder(Order order)
        {
            await _orderRepository.update(order);
        }


        public async Task<List<Order>> getOrderInTimeFrame(int yachtID, DateTime start, DateTime end)
        {
       
            return await _orderRepository.getOrderByTimeAndYacht(yachtID, start, end);

        }
        public Task<List<Order>> GetAllAcceptedOrders()
        {
            return _orderRepository.GetAllAcceptedOrders();
        }
        
        public async Task<bool> UpdateReturn(int orderId, bool newStatus)
        {
            await _orderRepository.UpdateReturn(orderId, newStatus);
            return newStatus;
        }

        public Task<List<Order>> GetAllUnRealeasedOrders()
        {
            return _orderRepository.GetAllUnRealeasedOrders();
        }

    }


}
