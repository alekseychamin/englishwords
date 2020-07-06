using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validations
{
    public class ExistIdCategoryValidation : IExistIdCategoryValidation
    {
        private readonly IRuleExistIdCategory _ruleExistIdCategory;

        public ExistIdCategoryValidation(IRuleExistIdCategory ruleExistIdCategory)
        {
            _ruleExistIdCategory = ruleExistIdCategory;
        }

        public void Invoke(IValidationDictionary validationDictionary, int id)
        {
            if (!_ruleExistIdCategory.IsValid(id))
                validationDictionary.AddError("Id", $"Category with id: {id} doesn`t exist");
        }
    }
}
