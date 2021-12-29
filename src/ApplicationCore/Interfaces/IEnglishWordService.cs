using ApplicationCore.Entities;
using ApplicationCore.Specifications.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEnglishWordService : IBaseService<EnglishWord>
    {
        Task<List<EnglishWord>> ListAsync(EnglishWordFilter filter, CancellationToken cancellationToken = default);
    }
}
