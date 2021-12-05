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
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateEnglishWordRequest>
        .WithActionResult<UpdateEnglishWordResult>
    {
        private readonly IRepository<EnglishWord> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<Update> _logger;

        public Update(IRepository<EnglishWord> repository, IMapper mapper, ILogger<Update> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/[namespace]")]
        [SwaggerOperation(
            Summary = "Updates an English Word Item",
            Description = "Updates an English Word Item",
            OperationId = "englishwords.update",
            Tags = new[] { "EnglishWords" })
        ]
        public override async Task<ActionResult<UpdateEnglishWordResult>> HandleAsync(UpdateEnglishWordRequest request, CancellationToken cancellationToken = default)
        {
            var word = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (word is null)
            {
                _logger.LogInformation($"EnglishWord with id = {request.Id} could not be found.");
                return NotFound();
            }

            word.Update(_mapper.Map<EnglishWordCoreDto>(request));

            await _repository.UpdateAsync(word, cancellationToken);

            return Ok(_mapper.Map<UpdateEnglishWordResult>(word));
        }
    }
}
