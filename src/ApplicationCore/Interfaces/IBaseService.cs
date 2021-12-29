using ApplicationCore.Specifications.Filter;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBaseService<T>
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);

        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
