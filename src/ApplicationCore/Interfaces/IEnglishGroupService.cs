using ApplicationCore.Entities;
using ApplicationCore.Specifications.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEnglishGroupService : IBaseService<EnglishGroup>
    {
        Task<List<EnglishGroup>> ListAsync(EnglishGroupFilter filter, CancellationToken cancellationToken = default);
    }
}
