using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Interface
{
    public interface IEmployeeRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);

        bool Add(T employee);
        bool Update(int id, T employee);
        bool Delete(int id);

    }
}
