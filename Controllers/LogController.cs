using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

namespace softlocke_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController: ControllerBase
    {
        MongoClient client = new MongoClient("mongodb+srv://r3s:Nar1_nar@cluster0.nm5x3ov.mongodb.net/?retryWrites=true&w=majority");
        
        [HttpGet]
        public ActionResult<List<LogItem>> Get()
        {
         var projects = client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").Find(LogItem => true).ToList(); 
            return Ok(projects);
           // return Ok(projects);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<LogItem> Get(string id)
        {
        var projectItem = client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").Find(LogItem => LogItem.Id == id).FirstOrDefault();
        return projectItem == null ? NotFound() : Ok(projectItem);
        }

        [HttpGet]
        [Route("lu/{id}")]
        public ActionResult<List<LogItem>> GetThruUser(string id)
        {
        var projectItem = client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").Find(LogItem => LogItem.PosterId == id).ToList();
        return projectItem == null ? NotFound() : Ok(projectItem);
        }

    [HttpPost]
    public ActionResult Post(LogItem logItem)
    {
        logItem.Id = ObjectId.GenerateNewId().ToString();

     //   var existingprojectItem = client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").Find(LogItem => LogItem.Id == logItem.Id).FirstOrDefault();
     //   if (existingprojectItem != null)
     //   {
     //       return Conflict("Whoops... That ID already exists!");
     //   }
     //   else
     //   {
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").InsertOne(logItem);
            var resourceUrl = Request.Path.ToString() + '/' + logItem.Id;
            return Created(resourceUrl, logItem.Id);
    //    }
    }

    [HttpPut]
    public ActionResult Put(LogItem projectItem)
    {
        var existingprojectItem = client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").Find(LogItem => LogItem.Id == projectItem.Id).FirstOrDefault();
        if (existingprojectItem == null)
        {
            return BadRequest("A rift has appeared in the space-time continuum. The log requested does not exist!");
        }
        else
        {
             try {
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").UpdateOne(Builders<LogItem>.Filter.Eq("Id", projectItem.Id), Builders<LogItem>.Update.Set("Name", projectItem.Name));
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").UpdateOne(Builders<LogItem>.Filter.Eq("Id", projectItem.Id), Builders<LogItem>.Update.Set("Content", projectItem.Content));
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").UpdateOne(Builders<LogItem>.Filter.Eq("Id", projectItem.Id), Builders<LogItem>.Update.Set("Topic", projectItem.Topic));
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").UpdateOne(Builders<LogItem>.Filter.Eq("Id", projectItem.Id), Builders<LogItem>.Update.Set("Tags", projectItem.Tags));
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").UpdateOne(Builders<LogItem>.Filter.Eq("Id", projectItem.Id), Builders<LogItem>.Update.Set("LinkNames", projectItem.LinkNames));
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").UpdateOne(Builders<LogItem>.Filter.Eq("Id", projectItem.Id), Builders<LogItem>.Update.Set("Links", projectItem.Links));
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").UpdateOne(Builders<LogItem>.Filter.Eq("Id", projectItem.Id), Builders<LogItem>.Update.Set("Images", projectItem.Images));
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").UpdateOne(Builders<LogItem>.Filter.Eq("Id", projectItem.Id), Builders<LogItem>.Update.Set("SnippetNames", projectItem.SnippetNames));
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").UpdateOne(Builders<LogItem>.Filter.Eq("Id", projectItem.Id), Builders<LogItem>.Update.Set("Snippets", projectItem.Snippets));
            return Ok();

            } catch (Exception e)

            {
                System.Diagnostics.Debug.WriteLine(e);
                return NotFound();
            }

            
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete(string Id)
    {
        var existinglogItem = client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").Find(LogItem => LogItem.Id == Id).FirstOrDefault();
        if (existinglogItem == null)
        {
            return NotFound("The log escaped your clutches... because it doesn't exist!");
        }
        else
        {
            client.GetDatabase("r3sDatabase").GetCollection<LogItem>("Logs").DeleteOne(existinglogItem.ToBsonDocument());

            return Ok();
        }
    }
    }
}