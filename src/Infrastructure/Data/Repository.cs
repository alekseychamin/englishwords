using ApplicationCore.Interfaces;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.DataAccess;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public Repository(EnglishWordDbContext dbContext) : base(dbContext)
        {

        }

        public string GroupNotFoundMessage => groupNotFoundMessage;

        public string WordNotFoundMessage => wordNotFoundMessage;

        public async Task<T> GetByIdAsync<TId>(TId id, string notFoundMessage, CancellationToken cancellationToken = default) where TId : notnull
        {
            var item = await GetByIdAsync(id, cancellationToken);

            if (item is null)
            {
                throw new KeyNotFoundException(notFoundMessage);
            }

            return item;
        }

        public async Task<T> GetBySpecAsync<Spec>(Spec specification, string notFoundMessage, CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification, ISpecification<T>
        {
            var item = await GetBySpecAsync(specification, cancellationToken);

            if (item is null)
            {
                throw new KeyNotFoundException(notFoundMessage);
            }

            return item;
        }

        private const string groupNotFoundMessage = "EnglishGroup with id = {0} not found.";
        private const string wordNotFoundMessage = "EnglishWord with id = {0} not found.";
    }
}
