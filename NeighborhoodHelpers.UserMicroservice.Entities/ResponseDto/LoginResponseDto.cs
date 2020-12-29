using System;
using System.Collections.Generic;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.Entities.ResponseDto
{
    public class LoginResponseDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public UserAddress Address { get; set; }

        public string Password { get; set; }

        public string HashKey { get; set; }
    }

    public class UserAddress
    {
        public string Address { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PinCode { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}
