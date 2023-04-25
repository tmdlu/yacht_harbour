using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccess.Models;
using Repository.Repositories;
namespace Repository.Services
{
    public class FunctionService
    {
        private readonly IFunctionRepository _functionRepository;


        public FunctionService(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }

        public Task<List<Function>> GetFunctions()
        {
            return _functionRepository.GetAll();
        }

        public async Task<List<Function>> GetFunctionsOfUser(int id)
        {
            return await _functionRepository.GetFunctionsOfUser(id);
        }
    }
}
