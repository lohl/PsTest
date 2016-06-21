using System;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using CodingTest.Models;

namespace CodingTest.Repositories
{
    public class MagRepository : RepositoryBase<Reading>, IMagRepository
    {
        public MagRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}