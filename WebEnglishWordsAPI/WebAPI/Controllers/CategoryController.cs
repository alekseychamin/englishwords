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
    public class CategoryController : ControllerBase, ICRUDController<CategoryView, CategoryCreate, CategoryUpdate>
    {
        private readonly IRepositoryBL<CategoryBL> _repositoryBL;
        private readonly IMapper _mapper;
        private readonly IUniqueValidation<CategoryBL> _uniqueCategoryValidation;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IRepositoryBL<CategoryBL> repositoryBL, IMapper mapper,
                                  IUniqueValidation<CategoryBL> uniqueCategoryValidation, 
                                  ILogger<CategoryController> logger)
        {
            _repositoryBL = repositoryBL;
            _mapper = mapper;
            _uniqueCategoryValidation = uniqueCategoryValidation;
            _logger = logger;
        }

        // POST api/category
        [HttpPost]
        public ActionResult<CategoryView> CreateItem(CategoryCreate itemCreate)
        {
            var itemBL = _mapper.Map<CategoryBL>(itemCreate);

            CustomValidateModel(ModelState, itemBL);

            if (!this.IsValidModel(_logger, itemBL))
                return BadRequest(ModelState);

            _repositoryBL.Create(itemBL);

            var itemView = _mapper.Map<CategoryView>(itemBL);

            return CreatedAtRoute("GetCategory", new { Id = itemView.Id }, itemView);
        }

        // DELETE api/category/5
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(int id)
        {
            if (!_repositoryBL.Delete(id))
                return NotFound();

            return Ok($"Category with id: {id} deleted");
        }

        // GET api/category/5
        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<CategoryView> GetItemById(int id)
        {
            var itemBL = _repositoryBL.Read(id);

            if (itemBL is null)
                return NotFound();

            return Ok(_mapper.Map<CategoryView>(itemBL));
        }

        // GET: api/category
        [HttpGet]
        public ActionResult<IEnumerable<CategoryView>> GetItems()
        {
            var itemsBL = _repositoryBL.GetAll();

            return Ok(_mapper.Map<IEnumerable<CategoryView>>(itemsBL));
        }

        // PUT api/category/5
        [HttpPut("{id}")]
        public ActionResult UpdateItem(int id, CategoryUpdate itemUpdate)
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

            var itemView = _mapper.Map<CategoryView>(itemBL);

            return Ok(itemView);
        }

        private void CustomValidateModel(ModelStateDictionary modelState, CategoryBL itemBL)
        {
            var modelStateWrapper = new ModelStateWrapper(modelState);

            _uniqueCategoryValidation.Invoke(modelStateWrapper, itemBL);
        }
    }
}
