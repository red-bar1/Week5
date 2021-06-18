using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEsamiStudente.Repositories
{
    public interface IRepository<T>
    {
        public IList<T> GetAll();
        public T GetByID(int ID);
        public bool Add(T item);
    }
}
