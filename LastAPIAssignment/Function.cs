using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Amazon.Lambda.Core;
using System.Dynamic;
using Newtonsoft.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LastAPIAssignment
{
    [Serializable]
    class Book
    {
        public string rank;
        public int rank_last_week;
        public int weeks_on_list;
        public string publisher;
        public string title;
        public string author;
        public string primary_isbn10;
        public string primary_isbn13;
    }
    public class Function
    {
        public static readonly HttpClient client = new HttpClient();
        private static AmazonDynamoDBClient dbClient = new AmazonDynamoDBClient();
        private string tableName = "LastAPI";
        public async Task<string> FunctionHandler(string input, ILambdaContext context)  //Task<string>
        {
            Table lastAPI = Table.LoadTable(dbClient, tableName);
            string bookString = await client.GetStringAsync("https://api.nytimes.com/svc/books/v3/lists/current/hardcover-fiction.json?api-key=wjg42vEM5uB3ALAX10TkaAArvtGu7G9C");
            Book myBook = JsonConvert.DeserializeObject<Book>(bookString);
            //PutItemOperationConfig config = new PutItemOperationConfig();
            string[] bookArray = bookString.Split('~');
            foreach (var x in bookArray)
            {
                return x;
                //myBook.rank = Array.Find(bookArray, x => x.Contains("Rank"));
                //Console.WriteLine(myBook.rank);
            }
            //config.ReturnValues = ReturnValues.AllOldAttributes;
            //Document result = await lastAPI.PutItemAsync(Document.FromJson(JsonConvert.SerializeObject(myBook)), config);
            return 0.ToString();
            //Table lastAPI = Table.LoadTable(dbClient, tableName);
            //Book myBook = JsonConvert.DeserializeObject<Book>(bookString);
            //string[] bookArray = new string[myBook];
            
            //PutItemOperationConfig config = new PutItemOperationConfig();
            //config.ReturnValues = ReturnValues.AllOldAttributes;
            //Document result = await lastAPI.PutItemAsync(Document.FromJson(JsonConvert.SerializeObject(myBook)), config);
            //return result.ToJson();
            //dynamic bookObject = JsonConvert.DeserializeObject<ExpandoObject>(bookString);
            //return bookObject;
            //return result;
        }
        //public async Task<string> FunctionHandler2(string bookObject, ILambdaContext context)
        //{
        //    Table lastAPI = Table.LoadTable(dbClient, tableName);
        //    Book myBook = JsonConvert.DeserializeObject<Book>(bookObject);
        //    PutItemOperationConfig config = new PutItemOperationConfig();
        //    config.ReturnValues = ReturnValues.AllOldAttributes;
        //    Document result = await lastAPI.PutItemAsync(Document.FromJson(JsonConvert.SerializeObject(myBook)), config);
        //    return result.ToJson();
        //}
    }
}
