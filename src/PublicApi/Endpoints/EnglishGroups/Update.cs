using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateEnglishGroupRequest>
        .WithActionResult<UpdateEnglishGroupResult>
    {
        private readonly IEnglishGroupService _englishGroupService;
        private readonly IMapper _mapper;

        public Update(IEnglishGroupService englishGroupService, IMapper mapper)
        {
            _englishGroupService = englishGroupService;
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
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var group = await _englishGroupService.GetByIdAsync(request.Id, cancellationToken);

            _mapper.Map(request, group);

            await _englishGroupService.UpdateAsync(group, cancellationToken);

            return Ok(_mapper.Map<UpdateEnglishGroupResult>(group));
        }
    }
}
