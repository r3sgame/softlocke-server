using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

namespace softlocke_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
    MongoClient client = new MongoClient("mongodb+srv://r3s:Nar1_nar@cluster0.nm5x3ov.mongodb.net/?retryWrites=true&w=majority");

        [HttpGet]
        [Route("Fire/{id}")]
        public ActionResult<UserItem> GetThruFirebase(string id)
        {
        var projectItem = client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").Find(UserItem => UserItem.FirebaseId == id).FirstOrDefault();
        return projectItem == null ? NotFound() : Ok(projectItem);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<UserItem> Get(string id)
        {
        
        var projectItem = client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").Find(UserItem => UserItem.Id == id).FirstOrDefault();
        return projectItem == null ? NotFound() : Ok(projectItem);
        
        }

    [HttpPost]
    public ActionResult Post(UserItem userItem)
    {
        userItem.Id = ObjectId.GenerateNewId().ToString();

        var existingprojectItem = client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").Find(UserItem => UserItem.Id == userItem.Id).FirstOrDefault();
        if (existingprojectItem != null)
        {
            return Conflict("Whoops... That ID already exists! Please try again.");
        }
        else
        {
            client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").InsertOne(userItem);
            var resourceUrl = Request.Path.ToString() + '/' + userItem.Id;
            return Created(resourceUrl, userItem);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult Put(UserItem projectItem)
    {
        var existingprojectItem = client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").Find(UserItem => UserItem.Id == projectItem.Id).FirstOrDefault();
        if (existingprojectItem == null)
        {
            return BadRequest("It seems this project does not exist...");
        }
        else
        {
            try {
            client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").UpdateOne(Builders<UserItem>.Filter.Eq("Id", projectItem.Id), Builders<UserItem>.Update.Set("Username", projectItem.Username));
            client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").UpdateOne(Builders<UserItem>.Filter.Eq("Id", projectItem.Id), Builders<UserItem>.Update.Set("Bio", projectItem.Bio));
            client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").UpdateOne(Builders<UserItem>.Filter.Eq("Id", projectItem.Id), Builders<UserItem>.Update.Set("LinkNames", projectItem.LinkNames));
            client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").UpdateOne(Builders<UserItem>.Filter.Eq("Id", projectItem.Id), Builders<UserItem>.Update.Set("Links", projectItem.Links));
            client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").UpdateOne(Builders<UserItem>.Filter.Eq("Id", projectItem.Id), Builders<UserItem>.Update.Set("Avatar", projectItem.Avatar));
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
        var existingprojectItem = client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").Find(UserItem => UserItem.Id == Id).FirstOrDefault();
        if (existingprojectItem == null)
        {
            return NotFound("Doesn't exist!");
        }
        else
        {
            client.GetDatabase("r3sDatabase").GetCollection<UserItem>("Users").DeleteOne(existingprojectItem.ToBsonDocument());

            return Ok("This project has been sent to the shadow realm.");
        }
    }
    }
    }
