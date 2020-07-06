using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IDataManager _dateManager;
        private readonly ILogger<OperationController> _logger;

        public OperationController(IDataManager dataManager, ILogger<OperationController> logger)
        {
            _dateManager = dataManager;
            _logger = logger;
        }
        
        [HttpGet("addenglishword")]
        public ActionResult AddEnglishWordToDb()
        {
            var count = _dateManager.AddEnglishWordsToDb();

            _logger.LogInformation("Added {0} english words", count);

            return Ok(new { Title = $"Added {count} english words" });
        }
    }
}
