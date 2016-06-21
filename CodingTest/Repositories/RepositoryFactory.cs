using CodingTest.Controllers;

namespace CodingTest.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DataContext _context;
        public RepositoryFactory(DataContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T: class
        {
            return (IRepository<T>)new RepositoryBase<T>(_context);
        }
    }
}