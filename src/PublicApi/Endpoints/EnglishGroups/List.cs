using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Filter;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PublicApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class List : EndpointBaseAsync
        .WithRequest<EnglishGroupFilterRequest>
        .WithActionResult<List<EnglishGroupDto>>
    {
        private readonly IEnglishGroupService _englishGroupService;
        private readonly IMapper _mapper;

        public List(IEnglishGroupService englishGroupService, IMapper mapper)
        {
            _englishGroupService = englishGroupService;
            _mapper = mapper;
        }
        
        [HttpGet("api/[namespace]")]
        [SwaggerOperation(
            Summary = "Get a List of English Groups",
            Description = "Get a List of English Groups",
            OperationId = "englishgroups.list",
            Tags = new[] { "EnglishGroups" })
        ]
        public override async Task<ActionResult<List<EnglishGroupDto>>> HandleAsync([FromQuery] EnglishGroupFilterRequest request, 
            CancellationToken cancellationToken = default)
        {
            var filter = _mapper.Map<EnglishGroupFilter>(request);

            var englishGroups = await _englishGroupService.ListAsync(filter, cancellationToken);
            
            return Ok(_mapper.Map<List<EnglishGroupDto>>(englishGroups));
        }
    }
}
