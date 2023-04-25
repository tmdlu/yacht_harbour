using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;
namespace Repository.Services
{
    public class StatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public Task<List<Status>> GetStatuses()
        {
            return _statusRepository.GetAll();
        }

        public Task<List<Status>> GetYachtStatuses(int id)
        {
            return _statusRepository.GetAll();
        }


        public async Task<Status> AddNewStatus(DateTime date, string condition, int yachtId, bool sailing_possibility, int accountId)
        {
            var newStatus = new Status()
            {
                StatusDate = date,
                Conditon = condition,
                YachtIdYacht = yachtId,
                SailingPossibility= sailing_possibility,
                AccountIdAccount=accountId


            };

            var entity = await _statusRepository.Insert(newStatus);
            return newStatus;
        }
        public async Task<List<Status>> getYachtStatuses(int id)
        {
            return await _statusRepository.GetYachtStatuses(id);
        }

        public async Task<Status> getYachtLastStatus(int id)
        {
            return await _statusRepository.GetLastYachtStatus(id);
        }
        public Task<List<Status>> GetUserStatuses(int id)
        {
            return _statusRepository.GetUserStatuses(id);
        }

    }
}
