namespace CodingTest.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T: class;
    }
}