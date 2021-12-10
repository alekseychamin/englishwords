using ApplicationCore.Entities;
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
    public class Delete : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<DeleteEnglishWordResult>
    {
        private readonly IRepository<EnglishWord> _repository;

        public Delete(IRepository<EnglishWord> repository)
        {
            _repository = repository;            
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
            var itemToDelete = await _repository.GetByIdAsync(wordId, cancellationToken);

            if (itemToDelete is null)
            {
                throw new KeyNotFoundException($"EnglishWord with id = {wordId} could not be found.");
            }

            await _repository.DeleteAsync(itemToDelete, cancellationToken);

            return Ok(new DeleteEnglishWordResult() { Id = wordId });
        }
    }
}
