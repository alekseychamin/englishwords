using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IDataManagerService _dataManagerService;
        private readonly ILogger<OperationController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public OperationController(IConfiguration configuration, IDataManagerService dataManagerService, 
                                   ILogger<OperationController> logger, IMapper mapper)
        {
            _dataManagerService = dataManagerService;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        [HttpGet("addenglishword")]
        public ActionResult AddEnglishWordToDb()
        {
            var fileName = _configuration.GetValue<string>("CSVFileName");
            var count = _dataManagerService.AddEnglishWordsToDb(fileName);

            _logger.LogInformation("Added {0} english words", count);

            return Ok(new { Title = $"Added {count} english words" });
        }

        [HttpGet("randomword")]
        public ActionResult<EnglishWordView> GetRandomEnglishWord([FromQuery] int categoryId)
        {
            var itemBL = _dataManagerService.GetRandomEnglishWord(categoryId);

            return Ok(_mapper.Map<EnglishWordView>(itemBL));
        }
    }
}
