using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

// polaczenie zamowien z yachtami i yach_statusami
namespace Repository.Services
{

    public class OrderManagmentService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IYachtRepository _yachtRepository;
        private readonly IStatusRepository _statuRepository;

        public OrderManagmentService(IOrderRepository orderRepository, IYachtRepository yachtRepository, IStatusRepository statuRepository)
        {
            _orderRepository = orderRepository;
            _yachtRepository = yachtRepository;
            _statuRepository = statuRepository;
        }

        

    }
}
