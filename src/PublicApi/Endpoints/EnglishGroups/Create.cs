using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class Create : EndpointBaseAsync
        .WithRequest<CreateEnglishGroupRequest>
        .WithActionResult<CreateEnglishGroupResult>
    {
        private readonly IEnglishGroupService _englishGroupService;
        private readonly IMapper _mapper;

        public Create(IEnglishGroupService englishGroupService, IMapper mapper)
        {
            _englishGroupService = englishGroupService;
            _mapper = mapper;
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
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<EnglishGroup>(request);

            var group = await _englishGroupService.AddAsync(entity, cancellationToken);

            return Ok(_mapper.Map<CreateEnglishGroupResult>(group));
        }
    }
}
