using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;
namespace Repository.Services
{
    public class YachtService
    {
        private readonly IYachtRepository _yachtRepository;

        public YachtService(IYachtRepository yachtRepository)
        {
            _yachtRepository = yachtRepository;
        }

        public Task<List<Yacht>> GetYachts()
        {
            return _yachtRepository.GetAll();
        }

        public async Task<Yacht> getYachtById(int id)
        {
            return await _yachtRepository.GetById(id);
        }
       

        public async Task<Yacht> AddNewYacht(string name, string type, float length, float width, float draft,float sailedSurface, int crewNumber, string remarks)
        {
            var newYacht = new Yacht()
            {
               YachtName= name, 
               Type= type,
               Length = length,
               Width = width,
               Draft = draft, 
               SailedSurface = sailedSurface, 
               CrewNumber= crewNumber,
               Remarks= remarks

            };

            var entity = await _yachtRepository.Insert(newYacht);
            return newYacht;
        }
        public async Task<Yacht> UpdateYacht(Yacht newYacht)
        {
           return await _yachtRepository.Update(newYacht);
        }

        public async Task<bool> UpdateAvailable(int yachtId, bool newStatus)
        {
            await _yachtRepository.UpdateAvailable(yachtId, newStatus);
            return newStatus;
        }
     
    }
}
