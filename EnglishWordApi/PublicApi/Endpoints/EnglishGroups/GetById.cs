using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublicApi.EnglishGroupEndpoints;
using Swashbuckle.AspNetCore.Annotations;
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
        private readonly ILogger<GetById> _logger;

        public GetById(IRepository<EnglishGroup> repository, IMapper mapper, ILogger<GetById> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("api/[namespace]/{groupId}")]
        [SwaggerOperation(
            Summary = "Get an English Group by Id",
            Description = "Get an English Group by Id with English Words",
            OperationId = "englishgroups.GetById")
        ]
        public override async Task<ActionResult<GetByIdEnglishGroupResult>> HandleAsync([FromRoute] int groupId, CancellationToken cancellationToken = default)
        {
            var group = await _repository.GetBySpecAsync(new EnglishGroupWithItemsSpecification(groupId), cancellationToken);
            
            if (group is null)
            {
                _logger.LogInformation($"EnglishGroup with id = {groupId} could not be found.");
                return NotFound(); 
            }

            return Ok(_mapper.Map<GetByIdEnglishGroupResult>(group));
        }
    }
}
