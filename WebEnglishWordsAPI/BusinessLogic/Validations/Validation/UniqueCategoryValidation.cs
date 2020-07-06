using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validations
{
    public class UniqueCategoryValidation : IUniqueValidation<CategoryBL>
    {
        private readonly IRuleUniqueValidation<CategoryBL> _ruleUniqueValidation;

        public UniqueCategoryValidation(IRuleUniqueValidation<CategoryBL> ruleUniqueValidation)
        {
            _ruleUniqueValidation = ruleUniqueValidation;
        }
        public void Invoke(IValidationDictionary validationDictionary, CategoryBL item)
        {
            if (!_ruleUniqueValidation.IsValid(item))
                validationDictionary.AddError("Name", $"Category with name: {item.Name} is exist");
        }
    }
}
