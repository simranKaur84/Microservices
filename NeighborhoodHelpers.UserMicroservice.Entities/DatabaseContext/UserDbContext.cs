using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeighborhoodHelpers.UserMicroservice.Entities.DatabaseContext
{
    public class UserDbContext : DbContext , IUserDbContext
    {
        private readonly IConfiguration _configuration;
        private AmazonDynamoDBClient _amazonDynamoDbClient;

        public UserDbContext(
            IConfiguration configuration
            )
        {
            _configuration = configuration;
        }

        public UserDbContext(
            IConfiguration configuration,
            AmazonDynamoDBClient amazonDynamoDbClient)
        {
            _configuration = configuration;
            _amazonDynamoDbClient = amazonDynamoDbClient;
        }

        /* Request to create the table on the Dynamo db */
        public async Task CreateTable(string tableName, string hashKey)
        {
            var accessKey = _configuration.GetSection("AwsCredentials").GetSection("AwsAccessKeyId").Value;
            var secretKey = _configuration.GetSection("AwsCredentials").GetSection("AwsSecretAccessKey").Value;
            _amazonDynamoDbClient = new AmazonDynamoDBClient(accessKey, secretKey,
            new AmazonDynamoDBConfig
            {
                ServiceURL = _configuration.GetSection("DynamoDb").GetSection("LocalServiceUrl").Value,
                UseHttp = Convert.ToBoolean(_configuration.GetSection("DynamoDb").GetSection("LocalMode").Value),
            });

            var client = GetAmazonDynamoDBClient();
            var tableResponse = await client.ListTablesAsync();
            if (!tableResponse.TableNames.Contains(tableName))
            {
                await client.CreateTableAsync(new CreateTableRequest
                {
                    TableName = tableName,
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 3,
                        WriteCapacityUnits = 1
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = hashKey,
                            KeyType = KeyType.HASH
                        }
                    },
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition { AttributeName = hashKey, AttributeType=ScalarAttributeType.S }
                    }
                });

                bool isTableAvailable = false;
                while (!isTableAvailable)
                {
                    Thread.Sleep(5000);
                    var tableStatus = await client.DescribeTableAsync(tableName);
                    isTableAvailable = tableStatus.Table.TableStatus == "ACTIVE";
                }
            }
        }

        public AmazonDynamoDBClient GetAmazonDynamoDBClient()
        {
            var accessKey = _configuration.GetSection("AwsCredentials").GetSection("AwsAccessKeyId").Value;
            var secretKey = _configuration.GetSection("AwsCredentials").GetSection("AwsSecretAccessKey").Value;
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.APSouth1);
            return client;
        }


        #region CRUD

        public void InsertItem<T>(T currentState)
        {
            var client = GetAmazonDynamoDBClient();
            DynamoDBContext _context = new DynamoDBContext(client);
            _context.SaveAsync<T>(currentState);
        }

        public Table GetAllItems(string tableName)
        {
            var client = GetAmazonDynamoDBClient();
            return Table.LoadTable(client, tableName);
        }

        public async Task<T> GetItemById<T>(Guid Id)
        {
            var client = GetAmazonDynamoDBClient();
            DynamoDBContext _context = new DynamoDBContext(client);
            return await _context.LoadAsync<T>(Id);
        }

        public async Task<List<T>> GetItemByString<T>(string key, string value)
        {
            var client = GetAmazonDynamoDBClient();
            DynamoDBContext _context = new DynamoDBContext(client);
            var scanConditions = new List<ScanCondition>();
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value)) 
                scanConditions.Add(new ScanCondition(key, ScanOperator.Equal, value));
            return await _context.ScanAsync<T>(scanConditions, null).GetRemainingAsync(); ;
        }

        #endregion

    }
}
