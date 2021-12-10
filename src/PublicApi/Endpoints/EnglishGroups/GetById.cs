using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublicApi.EnglishGroupEndpoints;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class GetById : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<GetByIdEnglishGroupResult>
    {
        private readonly IRepository<EnglishGroup> _repository;
        private readonly IMapper _mapper;

        public GetById(IRepository<EnglishGroup> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/[namespace]/{groupId}")]
        [SwaggerOperation(
            Summary = "Get an English Group by Id",
            Description = "Get an English Group by Id",
            OperationId = "englishgroups.GetById",
            Tags = new[] { "EnglishGroups" })
        ]
        public override async Task<ActionResult<GetByIdEnglishGroupResult>> HandleAsync([FromRoute] int groupId, CancellationToken cancellationToken = default)
        {
            var group = await _repository.GetByIdAsync(groupId, cancellationToken);
            
            if (group is null)
            {
                throw new KeyNotFoundException($"EnglishGroup with id = {groupId} could not be found.");
            }

            return Ok(_mapper.Map<GetByIdEnglishGroupResult>(group));
        }
    }
}
