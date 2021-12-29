using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishWords
{
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateEnglishWordRequest>
        .WithActionResult<UpdateEnglishWordResult>
    {
        private readonly IEnglishWordService _englishWordService;
        private readonly IMapper _mapper;

        public Update(IEnglishWordService englishWordService, IMapper mapper)
        {
            _englishWordService = englishWordService;
            _mapper = mapper;
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
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var word = await _englishWordService.GetByIdAsync(request.Id, cancellationToken);

            _mapper.Map(request, word);

            await _englishWordService.UpdateAsync(word, cancellationToken);

            return Ok(_mapper.Map<UpdateEnglishWordResult>(word));
        }
    }
}
