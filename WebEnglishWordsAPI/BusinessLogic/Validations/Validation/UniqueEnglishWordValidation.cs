using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validations
{
    public class UniqueEnglishWordValidation : IUniqueValidation<EnglishWordBL>
    {
        private readonly IRuleUniqueValidation<EnglishWordBL> _ruleUniqueValidation;

        public UniqueEnglishWordValidation(IRuleUniqueValidation<EnglishWordBL> ruleUniqueValidation)
        {
            _ruleUniqueValidation = ruleUniqueValidation;
        }

        public void Invoke(IValidationDictionary validationDictionary, EnglishWordBL item)
        {
            if (!_ruleUniqueValidation.IsValid(item))
                validationDictionary.AddError("WordPhrase", $"EnglisWord with word: {item.WordPhrase} is exist");
        }
    }
}
