using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishWords
{

    public class GetById : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<GetByIdEnglishWordResult>
    {
        private readonly IEnglishWordService _englishWordService;
        private readonly IMapper _mapper;

        public GetById(IEnglishWordService englishWordService, IMapper mapper)
        {
            _englishWordService = englishWordService;
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
            var word = await _englishWordService.GetByIdAsync(englishWordId, cancellationToken);

            return Ok(_mapper.Map<GetByIdEnglishWordResult>(word));
        }
    }
}
