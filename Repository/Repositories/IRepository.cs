using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IRepository<T1> where T1 : class
    {
        Task<List<T1>> GetAll();
        Task<T1> GetById(int id);
        Task<T1> Insert(T1 entity);
      
        Task Delete(int id);

        Task<T1> Update(T1 entity);

    }
}
