using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;
namespace Repository.Services
{
    public class CalendarService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IYachtRepository _yachtRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IStatusRepository _statusRepository;


        public CalendarService(IAccountRepository accountRepository, IYachtRepository yachtRepository, IOrderRepository orderRepository, IStatusRepository statusRepository)
        {
            _accountRepository = accountRepository;
            _yachtRepository = yachtRepository;
            _orderRepository = orderRepository;
            _statusRepository = statusRepository;
               
        }

        public async Task<List<Tuple<Status, string, string>>> GetStatusesWithNamesAndTypes()
        {
            List<Tuple<Status, string, string>> ResultStatuses = new List<Tuple<Status, string, string>>();
            try
            {
                var statuses = await _statusRepository.GetAllSortedStatuses();
                if(statuses is null)
                    throw new Exception("Statuses are NULL");

                foreach (var status in statuses)
                {
                    string name = "-";
                    string type = "-";
                    var orders = await _orderRepository.GetAll();
                    foreach (var order in orders)
                    {
                        
                        if (status.YachtIdYacht==order.YachtIdYacht && status.StatusDate>=order.StartDate && status.StatusDate <= order.EndDate)
                        {
                            var account = await _accountRepository.GetById(order.AccountIdAccount);
                             name = account.Name +" " + account.Surname;
                            type = order.OrderType;   
                            break;
                        }

                
                    }
                    ResultStatuses.Add(new Tuple<Status, string, string>(status, name,type));
                }





                return ResultStatuses;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
           

       
    }
}
