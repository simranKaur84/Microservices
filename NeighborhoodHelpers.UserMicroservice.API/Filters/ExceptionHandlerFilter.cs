using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NeighborhoodHelpers.UserMicroservice.Entities.Constants;
using NeighborhoodHelpers.UserMicroservice.Entities.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeighborhoodHelpers.UserMicroservice.API.Filters
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var error = new UserException()
            {
                HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = new string[] { ErrorMessages.DefaultErrorMessage }
            };
            context.Result = new JsonResult(error);
        }
    }
}
