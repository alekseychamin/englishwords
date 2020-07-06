using BusinessLogic.Model;
using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validations
{
    public class RuleExistIdCategory : IRuleExistIdCategory
    {
        private readonly IRepositoryBL<CategoryBL> _repositoryBL;

        public RuleExistIdCategory(IRepositoryBL<CategoryBL> repositoryBL)
        {
            _repositoryBL = repositoryBL;
        }
        public bool IsValid(int id)
        {
            var itemBL = _repositoryBL.Read(id);

            return (!(itemBL is null));
        }
    }
}
