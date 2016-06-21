using CodingTest.Models;

namespace CodingTest.Repositories
{
    public class GravRepository : RepositoryBase<ReadingGrav>, IGravRepository
    {
        public GravRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}