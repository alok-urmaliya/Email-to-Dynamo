using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EmailWebhook.Controllers;

[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
   
    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody]EmailData emailData)
    {
        //Please put your Accesskey and privatekey you got from AWS.
        var credentials = new BasicAWSCredentials("Your_Accesss_Key",
                                "your_Private_Key");

        //var kinesisClient = new AmazonKinesisClient(credentials, RegionEndpoint.USEast1);
        //var streamName = "emaildatastream";

        var dynamoClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
        

        //kinesisClient.CreateStream(new CreateStreamRequest
        //{
        //    StreamName = streamName,
        //    ShardCount = 1
        //});

        //API CODE
        if(emailData == null)
        {
            return BadRequest();
        }

        //var json = JsonConvert.SerializeObject(emailData);
        //var encode = Encoding.UTF8.GetBytes(json);

        //var data = new StringContent(json, Encoding.UTF8, "application/json");
        //var byteData = data.ReadAsByteArrayAsync();

        //kinesisClient.PutRecord(new PutRecordRequest
        //{
        //    StreamName = streamName,
        //    Data = new MemoryStream(encode),
        //    PartitionKey = Guid.NewGuid().ToString()
        //});

        Regex htmlTagRegex = new Regex("<.*?>", RegexOptions.Compiled);
        string filteredText = htmlTagRegex.Replace(emailData.body, string.Empty);

        var putItemRequest = new PutItemRequest
        {
            TableName = "EmailRecordTable",
            Item = new Dictionary<string, AttributeValue>
            {
                { "id", new AttributeValue { S = Guid.NewGuid().ToString() } },
                { "subject", new AttributeValue { S = emailData.subject} },
                { "body", new AttributeValue { S = filteredText} },
                { "from", new AttributeValue { S = emailData.from} },
                { "to", new AttributeValue { S = emailData.to} },
                { "Date", new AttributeValue { S = emailData.date} },
            }
        };

        var response = await dynamoClient.PutItemAsync(putItemRequest);
        return Ok();
    }
}