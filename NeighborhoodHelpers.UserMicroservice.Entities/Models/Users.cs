using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.Entities.Models
{
    [DynamoDBTable("Users")]
    public class Users
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("Id")]
        public Guid Id { get; set; }

        [DynamoDBProperty("FirstName")]
        public string FirstName { get; set; }

        [DynamoDBProperty("LastName")]
        public string LastName { get; set; }

        [DynamoDBProperty("Email")]
        public string Email { get; set; }

        [DynamoDBProperty("ContactNumber")]
        public string ContactNumber { get; set; }

        [Column("jsonb")]
        [DynamoDBProperty("Address")]
        public string Address { get; set; }

        [DynamoDBProperty("Password")]
        public string Password { get; set; }

        [DynamoDBProperty("OnBoarding")]
        public bool OnBoarding { get; set; }

        /// <summary>
        /// Common Entities
        /// </summary>
        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }

        /// <summary>
        /// Foreign Keys Constraints
        /// </summary>
        [ForeignKey("CreatedBy")]
        public Users CreatedUser { get; set; }

        [ForeignKey("UpdatedBy")]
        public Users UpdatedUser { get; set; }

    }
}
