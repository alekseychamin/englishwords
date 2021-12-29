using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishWords
{
    public class Create : EndpointBaseAsync
        .WithRequest<CreateEnglishWordRequest>
        .WithActionResult<CreateEnglishWordResult>
    {
        private readonly IEnglishWordService _englishWordService;
        private readonly IMapper _mapper;

        public Create(IEnglishWordService englishWordService, IMapper mapper)
        {
            _englishWordService = englishWordService;
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
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<EnglishWord>(request);

            var word = await _englishWordService.AddAsync(entity, cancellationToken);

            return Ok(_mapper.Map<CreateEnglishWordResult>(word));
        }
    }
}
