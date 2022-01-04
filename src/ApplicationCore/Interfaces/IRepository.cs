using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        string GroupNotFoundMessage { get; }
        string WordNotFoundMessage { get; }
        
        /// <summary>
        /// Throws KeyNotFoundException if item wtih id doesn't exist otherwise return an item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notFoundMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync<TId>(TId id, string notFoundMessage, CancellationToken cancellationToken = default) where TId : notnull;

        Task<T> GetBySpecAsync<Spec>(Spec specification, string notFoundMessage, CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification, ISpecification<T>;
    }
}
