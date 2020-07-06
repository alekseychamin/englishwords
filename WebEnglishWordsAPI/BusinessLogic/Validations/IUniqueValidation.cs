using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validations
{
    public interface IUniqueValidation<T> where T : class
    {
        void Invoke(IValidationDictionary validationDictionary, T item);
    }
}
