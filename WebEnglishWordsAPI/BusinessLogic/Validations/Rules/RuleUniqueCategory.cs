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
    public class RuleUniqueCategory : IRuleUniqueValidation<CategoryBL>
    {
        private readonly IRepositoryBL<CategoryBL> _repository;

        public RuleUniqueCategory(IRepositoryBL<CategoryBL> repository)
        {
            _repository = repository;
        }

        public bool IsValid(CategoryBL item)
        {
            var entity = _repository.GetAll().Where(x => x.Name.Equals(item.Name)).FirstOrDefault();

            return ((entity is null) || (entity.Id == item.Id));
        }
    }
}
