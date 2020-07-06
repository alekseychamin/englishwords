using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validations
{
    public interface IRuleExistIdCategory
    {
        bool IsValid(int id);
    }
}
