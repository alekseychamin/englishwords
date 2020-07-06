using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validations
{
    public interface IRuleUniqueValidation<T> where T : class
    {
        bool IsValid(T item);
    }
}
