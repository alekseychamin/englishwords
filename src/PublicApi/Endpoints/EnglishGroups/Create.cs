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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class Create : EndpointBaseAsync
        .WithRequest<CreateEnglishGroupRequest>
        .WithActionResult<CreateEnglishGroupResult>
    {
        private readonly IRepository<EnglishGroup> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<Create> _logger;

        public Create(IRepository<EnglishGroup> repository, IMapper mapper, ILogger<Create> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("api/[namespace]")]
        [SwaggerOperation(
            Summary = "Creates an English Group",
            Description = "Creates an English Group",
            OperationId = "englishgroups.create",
            Tags = new[] { "EnglishGroups" })
        ]
        public override async Task<ActionResult<CreateEnglishGroupResult>> HandleAsync(CreateEnglishGroupRequest request, CancellationToken cancellationToken = default)
        {
            var newItem = new EnglishGroup(_mapper.Map<EnglishGroupCoreDto>(request));

            await _repository.AddAsync(newItem, cancellationToken);

            return Ok(_mapper.Map<CreateEnglishGroupResult>(newItem));
        }
    }
}
