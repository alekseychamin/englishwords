using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using ApplicationCore.Specifications.Filter;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublicApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishWords
{
    public class List : EndpointBaseAsync
        .WithRequest<EnglishWordFilterRequest>
        .WithActionResult<EnglishWordListResult>
    {
        private readonly IRepository<EnglishWord> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<List> _logger;

        public List(IRepository<EnglishWord> repository, IMapper mapper, ILogger<List> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet("api/[namespace]")]
        [SwaggerOperation(
            Summary = "Get an List of English Words",
            Description = "Get an List of English Words",
            OperationId = "englishwords.list",
            Tags = new[] { "EnglishWords" })
        ]
        public override async Task<ActionResult<EnglishWordListResult>> HandleAsync([FromQuery] EnglishWordFilterRequest request, 
            CancellationToken cancellationToken = default)
        {
            var spec = new EnglishWordWithFilter(_mapper.Map<EnglishWordFilter>(request));
            var englishWords = await _repository.ListAsync(spec, cancellationToken);

            var result = new EnglishWordListResult()
            {
                EnglishWords = _mapper.Map<List<EnglishWordDto>>(englishWords)
            };

            return Ok(result);
        }
    }
}
