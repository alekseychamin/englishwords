﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using ApplicationCore.Specifications.Filter;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublicApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class List : EndpointBaseAsync
        .WithRequest<EnglishGroupFilterRequest>
        .WithActionResult<EnglishGroupListResult>
    {
        private readonly IRepository<EnglishGroup> _repository;
        private readonly IMapper _mapper;

        public List(IRepository<EnglishGroup> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet("api/[namespace]")]
        [SwaggerOperation(
            Summary = "Get a List of English Groups",
            Description = "Get a List of English Groups",
            OperationId = "englishgroups.list",
            Tags = new[] { "EnglishGroups" })
        ]
        public override async Task<ActionResult<EnglishGroupListResult>> HandleAsync([FromQuery] EnglishGroupFilterRequest request, 
            CancellationToken cancellationToken = default)
        {
            var spec = new EnglishGroupWithFilter(_mapper.Map<EnglishGroupFilter>(request));
            var englishGroups = await _repository.ListAsync(spec, cancellationToken);

            var result = new EnglishGroupListResult()
            {
                EnglishGroups = _mapper.Map<List<EnglishGroupDto>>(englishGroups)
            };
            
            return Ok(result);
        }
    }
}
