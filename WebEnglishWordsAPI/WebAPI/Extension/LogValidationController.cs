using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Extension
{
    public static class LogValidationController
    {
        public static bool IsValidModel<T>(this ControllerBase controller, ILogger<T> logger, object model)
        {
            if ((!controller.ModelState.IsValid) |
               ((controller.ObjectValidator != null) && (!controller.TryValidateModel(model))))
            {
                var allErrors = string.Join(",", controller.ModelState.Values
                                                           .SelectMany(s => s.Errors.Select(x => x.ErrorMessage)));

                logger.LogWarning("There are some validation problems: {0}", allErrors);

                return false;
            }

            return true;
        }
    }
}
