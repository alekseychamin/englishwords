using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class Delete : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<DeleteEnglishGroupResult>
    {
        private readonly IEnglishGroupService _englishGroupService;

        public Delete(IEnglishGroupService englishGroupService)
        {
            _englishGroupService = englishGroupService;
        }

        [HttpDelete("api/[namespace]/{groupId}")]
        [SwaggerOperation(
            Summary = "Deletes an English Group",
            Description = "Deletes an English Group",
            OperationId = "englishgroups.Delete",
            Tags = new[] { "EnglishGroups" })
        ]
        public override async Task<ActionResult<DeleteEnglishGroupResult>> HandleAsync([FromRoute] int groupId, CancellationToken cancellationToken = default)
        {
            await _englishGroupService.DeleteAsync(groupId, cancellationToken);

            return Ok(new DeleteEnglishGroupResult() { Id = groupId });
        }
    }
}
