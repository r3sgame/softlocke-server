
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNetCore.Http;

namespace softlocke_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController: ControllerBase
    {
        MongoClient client = new MongoClient("mongodb+srv://r3s:Nar1_nar@cluster0.nm5x3ov.mongodb.net/?retryWrites=true&w=majority");
        
        [HttpGet]
        public ActionResult<List<ProjectItem>> Get()
        {
            var projects = client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").Find(ProjectItem => true).ToList(); 
            
            return Ok(projects);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProjectItem> Get(string id)
        {
        var projectItem = client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").Find(ProjectItem => ProjectItem.Id == id).FirstOrDefault();
        return projectItem == null ? NotFound() : Ok(projectItem);
        }

        [HttpGet]
        [Route("pu/{id}")]
        public ActionResult<List<LogItem>> GetThruUser(string id)
        {
        var projectItem = client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").Find(ProjectItem => ProjectItem.AdminId == id).ToList();
        return projectItem == null ? NotFound() : Ok(projectItem);
        }

    [HttpPost]
    public ActionResult Post(ProjectItem projectItem)
    {
        projectItem.Id = ObjectId.GenerateNewId().ToString();
        
                client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").InsertOne(projectItem);
                var resourceUrl = Request.Path.ToString() + '/' + projectItem.Id;
                return Created(resourceUrl, "http://localhost:3000/tools/" + projectItem.Id);
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult Put(ProjectItem projectItem)
    {
        var existingprojectItem = client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").Find(ProjectItem => ProjectItem.Id == projectItem.Id).FirstOrDefault();
        if (existingprojectItem == null)
        {
            return BadRequest("It seems this project does not exist...");
        }
        else
        {
            try {
            client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").UpdateOne(Builders<ProjectItem>.Filter.Eq("Id", projectItem.Id), Builders<ProjectItem>.Update.Set("Name", projectItem.Name));
            client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").UpdateOne(Builders<ProjectItem>.Filter.Eq("Id", projectItem.Id), Builders<ProjectItem>.Update.Set("Paragraphs", projectItem.Paragraphs));
            client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").UpdateOne(Builders<ProjectItem>.Filter.Eq("Id", projectItem.Id), Builders<ProjectItem>.Update.Set("Blurb", projectItem.Blurb));
            client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").UpdateOne(Builders<ProjectItem>.Filter.Eq("Id", projectItem.Id), Builders<ProjectItem>.Update.Set("Command", projectItem.Command));
            client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").UpdateOne(Builders<ProjectItem>.Filter.Eq("Id", projectItem.Id), Builders<ProjectItem>.Update.Set("LinkNames", projectItem.LinkNames));
            client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").UpdateOne(Builders<ProjectItem>.Filter.Eq("Id", projectItem.Id), Builders<ProjectItem>.Update.Set("LinkPaths", projectItem.LinkPaths));
            client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").UpdateOne(Builders<ProjectItem>.Filter.Eq("Id", projectItem.Id), Builders<ProjectItem>.Update.Set("Images", projectItem.Images));
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
        var existingprojectItem = client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").Find(ProjectItem => ProjectItem.Id == Id).FirstOrDefault();
        if (existingprojectItem == null)
        {
            return NotFound("Doesn't exist!");
        }
        else
        {
            client.GetDatabase("r3sDatabase").GetCollection<ProjectItem>("Projects").DeleteOne(existingprojectItem.ToBsonDocument());

            return Ok("This project has been sent to the shadow realm.");
        }
    }
    }
}