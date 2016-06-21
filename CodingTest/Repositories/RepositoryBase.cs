using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CodingTest.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T: class 
    {
        private readonly DataContext _dataContext;

        public RepositoryBase(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<List<T>> GetAll()
        {
            return _dataContext.Set<T>().ToListAsync();
        }

        public Task<T> Get(int? id)
        {
            return  _dataContext.Set<T>().FindAsync(id);
        }

        public Task<int> Add(T item)
        {
            _dataContext.Set<T>().Add(item);
            return _dataContext.SaveChangesAsync();
        }

        public Task<int> Update(T item)
        {
            _dataContext.Entry(item).State = EntityState.Modified;
            return _dataContext.SaveChangesAsync();
        }

        public Task<int> Delete(T item)
        {
            _dataContext.Set<T>().Remove(item);
            return _dataContext.SaveChangesAsync();
        }
    }
}