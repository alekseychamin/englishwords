using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.DataAccess;

namespace Infrastructure.Data
{
    public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public Repository(EnglishWordDbContext dbContext) : base(dbContext)
        {

        }
    }
}
