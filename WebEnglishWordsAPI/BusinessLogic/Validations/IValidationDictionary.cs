using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validations
{
    public interface IValidationDictionary
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}
