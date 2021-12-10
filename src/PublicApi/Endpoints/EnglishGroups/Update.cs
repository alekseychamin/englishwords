using ApplicationCore.Entities;
using ApplicationCore.Entities.Dto;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateEnglishGroupRequest>
        .WithActionResult<UpdateEnglishGroupResult>
    {
        private readonly IRepository<EnglishGroup> _repository;
        private readonly IMapper _mapper;

        public Update(IRepository<EnglishGroup> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPut("api/[namespace]")]
        [SwaggerOperation(
            Summary = "Updates an English Group Item",
            Description = "Updates an English Group Item",
            OperationId = "englishgroups.update",
            Tags = new[] { "EnglishGroups" })
        ]
        public override async Task<ActionResult<UpdateEnglishGroupResult>> HandleAsync(UpdateEnglishGroupRequest request, CancellationToken cancellationToken = default)
        {
            var group = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (group is null)
            {
                throw new KeyNotFoundException($"EnglishGroup with id = {request.Id} could not be found.");
            }

            group.Update(_mapper.Map<EnglishGroupCoreDto>(request));

            await _repository.UpdateAsync(group, cancellationToken);

            return Ok(_mapper.Map<UpdateEnglishGroupResult>(group));
        }
    }
}
