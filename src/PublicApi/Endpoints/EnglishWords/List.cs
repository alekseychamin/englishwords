using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Filter;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PublicApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishWords
{
    public class List : EndpointBaseAsync
        .WithRequest<EnglishWordFilterRequest>
        .WithActionResult<List<EnglishWordDto>>
    {
        private readonly IEnglishWordService _englishWordService;
        private readonly IMapper _mapper;

        public List(IEnglishWordService englishWordService, IMapper mapper)
        {
            _englishWordService = englishWordService;
            _mapper = mapper;
        }
        
        [HttpGet("api/[namespace]")]
        [SwaggerOperation(
            Summary = "Get a List of English Words",
            Description = "Get a List of English Words",
            OperationId = "englishwords.list",
            Tags = new[] { "EnglishWords" })
        ]
        public override async Task<ActionResult<List<EnglishWordDto>>> HandleAsync([FromQuery] EnglishWordFilterRequest request, 
            CancellationToken cancellationToken = default)
        {
            var filter = _mapper.Map<EnglishWordFilter>(request);

            var englishWords = await _englishWordService.ListAsync(filter, cancellationToken);

            return Ok(_mapper.Map<List<EnglishWordDto>>(englishWords));
        }
    }
}
