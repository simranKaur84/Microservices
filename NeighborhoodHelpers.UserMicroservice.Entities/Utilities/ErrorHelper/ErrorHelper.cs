using NeighborhoodHelpers.UserMicroservice.Entities.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.Entities.Utilities.ErrorHelper
{
    public class ErrorHelper : IErrorHelper
    {
        public void HandleError(string errorTag, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            throw new UserException
            {
                HttpStatusCode = httpStatusCode,
                ErrorMessage = new[] { errorTag }
            };
        }
    }
}
