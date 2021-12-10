using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishWords
{

    public class GetById : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<GetByIdEnglishWordResult>
    {
        private readonly IRepository<EnglishWord> _repository;
        private readonly IMapper _mapper;

        public GetById(IRepository<EnglishWord> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/[namespace]/{englishWordId}")]
        [SwaggerOperation(
            Summary = "Get an English Word by Id",
            Description = "Get an English Word by Id",
            OperationId = "englishwords.GetById",
            Tags = new[] { "EnglishWords" })
        ]
        public override async Task<ActionResult<GetByIdEnglishWordResult>> HandleAsync([FromRoute] int englishWordId, CancellationToken cancellationToken = default)
        {
            var spec = new EnglishWordWithGroup(englishWordId);

            var word = await _repository.GetBySpecAsync(spec);

            if (word is null)
            {
                throw new KeyNotFoundException($"EnglishWord with id = {englishWordId} could not be found.");
            }

            return Ok(_mapper.Map<GetByIdEnglishWordResult>(word));
        }
    }
}
