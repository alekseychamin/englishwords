using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using ApplicationCore.Specifications.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class EnglishWordService : IEnglishWordService
    {
        private readonly IRepository<EnglishWord> _wordRepository;
        private readonly IRepository<EnglishGroup> _groupRepository;

        public EnglishWordService(IRepository<EnglishWord> wordRepository, IRepository<EnglishGroup> groupRepository)
        {
            _wordRepository = wordRepository;
            _groupRepository = groupRepository;
        }

        public async Task<EnglishWord> AddAsync(EnglishWord entity, CancellationToken cancellationToken)
        {
            if (entity.EnglishGroupId.HasValue)
            {
                await _groupRepository.GetByIdAsync(entity.EnglishGroupId, $"EnglishGroup with id = {entity.EnglishGroupId} not found.", cancellationToken);
            }

            return await _wordRepository.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _wordRepository.GetByIdAsync(id, $"EnglishWord with id = {id} not found.", cancellationToken);

            await _wordRepository.DeleteAsync(entity, cancellationToken);
        }

        public async Task<EnglishWord> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var spec = new EnglishWordWithGroup(id);

            return await _wordRepository.GetBySpecAsync(spec, $"EnglishWord with id = {id} not found.", cancellationToken);
        }

        public async Task<List<EnglishWord>> ListAsync(EnglishWordFilter filter, CancellationToken cancellationToken = default)
        {
            var spec = new EnglishWordWithFilter(filter);

            return await _wordRepository.ListAsync(spec, cancellationToken);
        }

        public async Task UpdateAsync(EnglishWord entity, CancellationToken cancellationToken = default)
        {
            if (entity.EnglishGroupId.HasValue)
            {
                await _groupRepository.GetByIdAsync(entity.EnglishGroupId,
                    $"EnglishGroup with id = {entity.EnglishGroupId} not found.", cancellationToken);
            }

            await _wordRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
