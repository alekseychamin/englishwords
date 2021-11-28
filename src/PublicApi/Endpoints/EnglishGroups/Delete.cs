using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class Delete : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<DeleteEnglishGroupResult>
    {
        private readonly IRepository<EnglishGroup> _repository;
        private readonly ILogger<Delete> _logger;

        public Delete(IRepository<EnglishGroup> repository, ILogger<Delete> logger)
        {
            _repository = repository;
            _logger = logger;
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
            var itemToDelete = await _repository.GetByIdAsync(groupId, cancellationToken);

            if (itemToDelete is null)
            {
                _logger.LogError($"EnglishGroup with id = {groupId} could not be found.");
                return NotFound();
            }

            await _repository.DeleteAsync(itemToDelete, cancellationToken);

            return Ok(new DeleteEnglishGroupResult() { Id = groupId });
        }
    }
}
