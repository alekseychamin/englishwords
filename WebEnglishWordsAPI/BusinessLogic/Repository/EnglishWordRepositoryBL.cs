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
    public class EnglishWordRepositoryBL : IRepositoryBL<EnglishWordBL>
    {
        private readonly IRepository<EnglishWord> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<EnglishWordRepositoryBL> _logger;

        public EnglishWordRepositoryBL(IRepository<EnglishWord> repository, IMapper mapper, 
                                       ILogger<EnglishWordRepositoryBL> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public void Create(EnglishWordBL item)
        {
            if (item is null)
            {
                _logger.LogError("Can`t create EnglishWord because item is null");
                throw new ArgumentNullException("item");
            }

            var itemDAL = _mapper.Map<EnglishWord>(item);

            _repository.Create(itemDAL);

            SaveChanges();

            _mapper.Map(itemDAL, item);
            
            _logger.LogInformation("Created new EnglishWord with id: {0}", itemDAL.Id);
        }

        public bool Delete(int id)
        {
            var result = _repository.Delete(id);

            if (result)
            {
                _logger.LogInformation("EnglishWord deleted with id: {0}", id);

                SaveChanges();
            }
            else
                _logger.LogWarning("Can`t find EnglishWord to delete with id: {0}", id);            

            return result;
        }

        public IEnumerable<EnglishWordBL> GetAll()
        {
            var itemsDAL = _repository.GetAll();

            return _mapper.Map<IEnumerable<EnglishWordBL>>(itemsDAL);
        }

        public EnglishWordBL Read(string word)
        {
            EnglishWordBL output = null;

            var itemsDAL = _repository.GetAll();

            if (itemsDAL is null)
                return output;

            var result = itemsDAL.Where(x => x.WordPhrase.Equals(word)).FirstOrDefault();

            output = _mapper.Map<EnglishWordBL>(result);

            return output;
        }

        public EnglishWordBL Read(int id)
        {
            var itemDAL = _repository.Read(id);

            if (itemDAL is null)
                _logger.LogWarning("Can`t find EnglishWord with id: {0}", id);

            return _mapper.Map<EnglishWordBL>(itemDAL);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public void Update(EnglishWordBL item)
        {
            var itemDAL = _repository.Read(item.Id);

            if (itemDAL is null)
            {
                _logger.LogError("Can`t find EnglishWord with id: {0}", item.Id);
                return;
            }

            _mapper.Map(item, itemDAL);

            SaveChanges();
        }
    }
}
