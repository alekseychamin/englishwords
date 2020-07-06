using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validations
{
    public interface IExistIdCategoryValidation
    {
        void Invoke(IValidationDictionary validationDictionary, int id);
    }
}
