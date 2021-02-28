using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KybInfrastructure.Service
{
    public class RequestModelActionFilterValidatorHelper
    {
        public static async Task CompleteActionFilterValidatorProcess<T>(List<Action<T, ValidationResult>> validatorMethods, ActionExecutingContext filterContext, ActionExecutionDelegate next) where T : class, new()
        {
            T requestModel = GetRequestModel<T>(filterContext);

            ValidationResult validationResult = ValidateRequestModel<T>(requestModel, validatorMethods);

            await FinalizeActionFilterValidator(validationResult, filterContext, next);
        }

        public static T GetRequestModel<T>(ActionExecutingContext filterContext) where T : class, new()
            => filterContext.ActionArguments.Values.FirstOrDefault() as T;

        public static ValidationResult ValidateRequestModel<T>(T requestModel, List<Action<T, ValidationResult>> validatorMethods)
        {
            ValidationResult validationResult = new ValidationResult();

            foreach (var validatorMethod in validatorMethods)
            {
                validatorMethod(requestModel, validationResult);

                if (!validationResult.IsValid)
                    break;
            }

            return validationResult;
        }

        public static async Task FinalizeActionFilterValidator(ValidationResult validationResult, ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            if (!validationResult.IsValid)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                filterContext.Result = new JsonResult(validationResult);
            }
            else
            { await next(); }
        }
    }
}