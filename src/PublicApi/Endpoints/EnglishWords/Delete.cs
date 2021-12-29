using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishWords
{
    public class Delete : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<DeleteEnglishWordResult>
    {
        private readonly IEnglishWordService _englishWordService;

        public Delete(IEnglishWordService englishWordService)
        {
            _englishWordService = englishWordService;
        }

        [HttpDelete("api/[namespace]/{wordId}")]
        [SwaggerOperation(
            Summary = "Deletes an English Word",
            Description = "Deletes an English Word",
            OperationId = "englishwords.Delete",
            Tags = new[] { "EnglishWords" })
        ]
        public override async Task<ActionResult<DeleteEnglishWordResult>> HandleAsync([FromRoute] int wordId, CancellationToken cancellationToken = default)
        {
            await _englishWordService.DeleteAsync(wordId, cancellationToken);

            return Ok(new DeleteEnglishWordResult() { Id = wordId });
        }
    }
}
