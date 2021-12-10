using ApplicationCore.Entities;
using ApplicationCore.Entities.Dto;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishWords
{
    public class Create : EndpointBaseAsync
        .WithRequest<CreateEnglishWordRequest>
        .WithActionResult<CreateEnglishWordResult>
    {
        private readonly IRepository<EnglishWord> _wordRepository;
        private readonly IRepository<EnglishGroup> _groupRepository;
        private readonly IMapper _mapper;

        public Create(IRepository<EnglishWord> wordRepository, IRepository<EnglishGroup> groupRepository, IMapper mapper)
        {
            _wordRepository = wordRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        [HttpPost("api/[namespace]")]
        [SwaggerOperation(
            Summary = "Creates an English Word",
            Description = "Creates an English Word",
            OperationId = "englishwords.create",
            Tags = new[] { "EnglishWords" })
        ]
        public override async Task<ActionResult<CreateEnglishWordResult>> HandleAsync(CreateEnglishWordRequest request, CancellationToken cancellationToken = default)
        {
            if (request.EnglishGroupId.HasValue)
            {
                var group = await _groupRepository.GetByIdAsync(request.EnglishGroupId, cancellationToken);

                if (group is null)
                {
                    throw new KeyNotFoundException($"EnglishGroup with id = {request.EnglishGroupId} could not be found.");
                }
            }
            
            var newItem = new EnglishWord(_mapper.Map<EnglishWordCoreDto>(request));

            await _wordRepository.AddAsync(newItem, cancellationToken);

            var word = await _wordRepository.GetByIdAsync(newItem.Id, cancellationToken);

            return Ok(_mapper.Map<CreateEnglishWordResult>(word));
        }
    }
}
