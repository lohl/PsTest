using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingTest.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(int? id);
        Task<int> Add(T item);
        Task<int> Update(T item);
        Task<int> Delete(T item);

    }
}