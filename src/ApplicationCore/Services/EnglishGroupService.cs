using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using ApplicationCore.Specifications.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class EnglishGroupService : IEnglishGroupService
    {
        private readonly IRepository<EnglishGroup> _groupRepository;
        private readonly IRepository<EnglishWord> _wordRepository;

        public EnglishGroupService(IRepository<EnglishGroup> groupRepository, IRepository<EnglishWord> wordRepository)
        {
            _groupRepository = groupRepository;
            _wordRepository = wordRepository;
        }

        public async Task<EnglishGroup> AddAsync(EnglishGroup entity, CancellationToken cancellationToken)
        {
            return await _groupRepository.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _groupRepository.GetByIdAsync(id, $"EnglishGroup with id = {id} not found.", cancellationToken);

            await _groupRepository.DeleteAsync(entity, cancellationToken);
        }

        public async Task<EnglishGroup> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var group = await _groupRepository.GetByIdAsync(id, $"EnglishGroup with id = {id} not found.", cancellationToken);
            group.CountWords = await _wordRepository.CountAsync(new EnglishWordsByGroup(id), cancellationToken);

            return group;
            
        }

        public async Task<List<EnglishGroup>> ListAsync(EnglishGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var spec = new EnglishGroupWithFilter(filter);

            var groups = await _groupRepository.ListAsync(spec, cancellationToken);

            groups
                .Select(async (x) => 
                { x.CountWords = await _wordRepository.CountAsync(new EnglishWordsByGroup(x.Id), cancellationToken); })
                .ToList();

            return groups;
        }

        public async Task UpdateAsync(EnglishGroup entity, CancellationToken cancellationToken = default)
        {
            await _groupRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
