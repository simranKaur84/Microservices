using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.Entities.Utilities.ErrorHelper
{
    public interface IErrorHelper
    {
        void HandleError(string errorTag, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest);
    }
}
