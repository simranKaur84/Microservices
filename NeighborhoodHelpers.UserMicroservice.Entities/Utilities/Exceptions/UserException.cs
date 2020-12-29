using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.Entities.Utilities.Exceptions
{
    public class UserException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public string[] ErrorMessage { get; set; }
    }
}
