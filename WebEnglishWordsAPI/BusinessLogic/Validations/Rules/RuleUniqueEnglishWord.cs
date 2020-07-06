using BusinessLogic.Model;
using BusinessLogic.Repository;
using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validations
{
    public class RuleUniqueEnglishWord : IRuleUniqueValidation<EnglishWordBL>
    {
        private readonly IRepositoryBL<EnglishWordBL> _repository;

        public RuleUniqueEnglishWord(IRepositoryBL<EnglishWordBL> repository)
        {
            _repository = repository;
        }
        public bool IsValid(EnglishWordBL item)
        {
            var entity = _repository.GetAll().Where(x => x.WordPhrase.Equals(item.WordPhrase)).FirstOrDefault();

            return ((entity is null) || (entity.Id == item.Id));
        }
    }
}
