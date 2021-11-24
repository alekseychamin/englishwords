﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using ApplicationCore.Specifications.Filter;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class List : EndpointBaseAsync
        .WithRequest<EnglishGroupFilterRequest>
        .WithResult<EnglishGroupListResult>
    {
        private readonly IRepository<EnglishGroup> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<List> _logger;

        public List(IRepository<EnglishGroup> repository, IMapper mapper, ILogger<List> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet("api/[namespace]")]
        public override async Task<EnglishGroupListResult> HandleAsync([FromQuery] EnglishGroupFilterRequest request, 
            CancellationToken cancellationToken = default)
        {
            var spec = new EnglishGroupWithFilter(_mapper.Map<EnglishGroupFilter>(request));
            var englishGroups = await _repository.ListAsync(spec, cancellationToken);

            var result = new EnglishGroupListResult()
            {
                EnglishGroups = _mapper.Map<List<EnglishGroupDto>>(englishGroups)
            };
            
            return result;
        }
    }
}