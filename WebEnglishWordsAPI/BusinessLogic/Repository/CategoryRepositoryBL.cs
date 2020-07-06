using AutoMapper;
using BusinessLogic.Model;
using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Repository
{
    public class CategoryRepositoryBL : IRepositoryBL<CategoryBL>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryRepositoryBL> _logger;
        
        public CategoryRepositoryBL(IRepository<Category> repository, IMapper mapper, 
                                    ILogger<CategoryRepositoryBL> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public void Create(CategoryBL item)
        {
            if (item is null)
            {
                _logger.LogError("Can`t create Category because item is null");
                throw new ArgumentNullException("item");
            }

            var itemDAL = _mapper.Map<Category>(item);

            _repository.Create(itemDAL);

            SaveChanges();

            _mapper.Map(itemDAL, item);

            _logger.LogInformation("Created new Category with id: {0}", itemDAL.Id);
        }

        public bool Delete(int id)
        {
            var result = _repository.Delete(id);

            if (result)
            {
                _logger.LogInformation("Category deleted with id: {0}", id);

                SaveChanges();
            }
            else
                _logger.LogWarning("Can`t find Category to delete with id: {0}", id);            

            return result;
        }

        public IEnumerable<CategoryBL> GetAll()
        {
            var itemsDAL = _repository.GetAll();

            return _mapper.Map<IEnumerable<CategoryBL>>(itemsDAL);
        }

        public CategoryBL Read(string word)
        {
            CategoryBL output = null;

            var itemsDAL = _repository.GetAll();

            if (itemsDAL is null)
                return output;

            var result = itemsDAL.Where(x => x.Name.Equals(word)).FirstOrDefault();

            output = _mapper.Map<CategoryBL>(result);

            return output;
        }

        public CategoryBL Read(int id)
        {
            var itemDAL = _repository.Read(id);

            if (itemDAL is null)
                _logger.LogWarning("Can`t find Category with id: {0}", id);

            return _mapper.Map<CategoryBL>(itemDAL);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public void Update(CategoryBL item)
        {
            var itemDAL = _repository.Read(item.Id);

            if (itemDAL is null)
            {
                _logger.LogError("Can`t find Category with id: {0}", item.Id);
                return;
            }

            _mapper.Map(item, itemDAL);

            SaveChanges();
        }
    }
}
