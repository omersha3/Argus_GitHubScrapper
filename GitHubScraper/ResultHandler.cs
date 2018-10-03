using MongoDB.Driver;
using MongoDB.Bson;
using System.IO;
using GitHubScraper;

public class ResultHandler
{

    public static void Create()
    {
         File.Delete("ResultTiming.txt");
    }


    public static void WriteResultTiming(string Title, string timing)
    {

        File.AppendAllLines("ResultTiming.txt", new[] { Title + "(MS): " + timing });
    }

    public static void InitMongo(string mongoIPandPort)
    {
        var connectionString = string.Format("mongodb://{0}", mongoIPandPort); 
        var client = new MongoClient(connectionString);
        client.DropDatabase("test");
    }

    public static void WriteToMongo(string mongoIPandPort, string Title, string Description,  string[] Tags , string Time, string Lang, string Star, bool IsValid)
        {
        var connectionString = string.Format("mongodb://{0}", mongoIPandPort); 
        var client = new MongoClient(connectionString);

        IMongoDatabase db = client.GetDatabase("test");


        var document = new BsonDocument
{
  {"Title",  BsonValue.Create(Title)},
  {"Description", BsonValue.Create(Description)},
  { "Tags", new BsonArray( Tags )},
   { "Time", new BsonString(Time) },
  { "Lang", new BsonString(Lang) },
  { "Star", new BsonString(Star) },
  { "IsValid", new BsonBoolean(IsValid) }

};
        var collection = db.GetCollection<BsonDocument>("GitRepo");

        collection.InsertOne(document);

    }
}