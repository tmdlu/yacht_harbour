using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;
namespace Repository.Services
{
    public class PowerService
    {
        private readonly IPowerRepository _powerRepository;
        private readonly IAccountRepository _accountRepository;
        public PowerService(IPowerRepository powerRepository, IAccountRepository accountRepository)
        {
            _powerRepository = powerRepository;
            _accountRepository = accountRepository;

        }
        public Task<List<Power>> GetAllPowers()
        {
            return _powerRepository.GetAll();
        }

        public async Task<List<Power>> GetUsersPowers(int id)
        {
            return await _powerRepository.GetPowersOfUser(id);
        }


        public async Task<List<Tuple<Power, int>>> GetPowersWithAccountID()
        {
            List<Tuple<Power, int>> result =new List<Tuple<Power, int>>();
            
            try
            {
                List<Account> accounts = await _accountRepository.GetAll();
                if(accounts is null)
                    throw new Exception("powers are NULL");
                foreach(Account account in accounts)
                {
                    var temp = await _powerRepository.GetPowersOfUser(account.IdAccount);
                    foreach(var temp1 in temp)
                    {
                        result.Add(new Tuple<Power, int>(temp1, account.IdAccount));
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
