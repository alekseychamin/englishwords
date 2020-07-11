using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Model;
using BusinessLogic.Repository;
using BusinessLogic.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using WebAPI.Extension;
using WebAPI.Model;
using WebAPI.Validation;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnglishWordController : ControllerBase, ICRUDController<EnglishWordView, EnglishWordCreate, EnglishWordUpdate>
    {
        private readonly IRepositoryBL<EnglishWordBL> _repositoryBL;
        private readonly IMapper _mapper;
        private readonly IUniqueValidation<EnglishWordBL> _uniqueEnglishWordValidation;
        private readonly IExistIdCategoryValidation _existIdCategoryValidation;
        private readonly ILogger<EnglishWordController> _logger;

        public EnglishWordController(IRepositoryBL<EnglishWordBL> repositoryBL, IMapper mapper,
                                     IUniqueValidation<EnglishWordBL> uniqueEnglishWordValidation,
                                     IExistIdCategoryValidation existIdCategoryValidation,
                                     ILogger<EnglishWordController> logger)
        {
            _repositoryBL = repositoryBL;
            _mapper = mapper;
            _uniqueEnglishWordValidation = uniqueEnglishWordValidation;
            _existIdCategoryValidation = existIdCategoryValidation;
            _logger = logger;
        }

        // POST api/englishword
        [HttpPost]
        public ActionResult<EnglishWordView> CreateItem(EnglishWordCreate itemCreate)
        {
            var itemBL = _mapper.Map<EnglishWordBL>(itemCreate);

            CustomValidateModel(ModelState, itemBL);

            if (!this.IsValidModel(_logger, itemBL))
                return BadRequest(ModelState);

            _repositoryBL.Create(itemBL);

            var itemView = _mapper.Map<EnglishWordView>(itemBL);

            return CreatedAtRoute("GetEnglishWord", new { Id = itemView.Id }, itemView);
        }

        // DELETE api/englishword/5
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(int id)
        {
            if (!_repositoryBL.Delete(id))
                return NotFound();

            return Ok($"English word with id: {id} deleted");
        }

        // GET api/englishword/5
        [HttpGet("{id}", Name = "GetEnglishWord")]
        public ActionResult<EnglishWordView> GetItemById(int id)
        {
            var itemBL = _repositoryBL.Read(id);

            if (itemBL is null)
                return NotFound();

            return Ok(_mapper.Map<EnglishWordView>(itemBL));
        }

        // GET: api/englishword
        [HttpGet]
        public ActionResult<IEnumerable<EnglishWordView>> GetItems()
        {
            var itemsBL = _repositoryBL.GetAll();

            return Ok(_mapper.Map<IEnumerable<EnglishWordView>>(itemsBL));
        }

        // PUT api/englishword/5
        [HttpPut("{id}")]
        public ActionResult UpdateItem(int id, EnglishWordUpdate itemUpdate)
        {
            var itemBL = _repositoryBL.Read(id);

            if (itemBL is null)
                return NotFound();

            _mapper.Map(itemUpdate, itemBL);

            CustomValidateModel(ModelState, itemBL);

            if (!this.IsValidModel(_logger, itemBL))
                return BadRequest(ModelState);

            _repositoryBL.Update(itemBL);
            
            itemBL = _repositoryBL.Read(id);

            var itemView = _mapper.Map<EnglishWordView>(itemBL);

            return Ok(itemView);
        }

        private void CustomValidateModel(ModelStateDictionary modelState, EnglishWordBL itemBL)
        {
            var modelStateWrapper = new ModelStateWrapper(modelState);

            _uniqueEnglishWordValidation.Invoke(modelStateWrapper, itemBL);
            _existIdCategoryValidation.Invoke(modelStateWrapper, itemBL.CategoryId);
        }
    }
}
