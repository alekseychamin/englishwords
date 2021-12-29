using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IEnglishGroupService _englishGroupService;
        private readonly IMapper _mapper;

        public GetById(IEnglishGroupService englishGroupService, IMapper mapper)
        {
            _englishGroupService = englishGroupService;
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
            var group = await _englishGroupService.GetByIdAsync(groupId, cancellationToken);

            return Ok(_mapper.Map<GetByIdEnglishGroupResult>(group));
        }
    }
}
