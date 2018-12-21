using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroToAPIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntroToAPIs.Controllers
{
  [Route("api/[controller]")]
  // localhost:5000/api/animals
  [ApiController]
  public class AnimalsController : ControllerBase
  {
    //GET that returns all Animals 
    //GET /api/people
    [HttpGet]
    public ActionResult<IEnumerable<SeenAnimals>> GetAction()
    {
      // query my database
      var db = new SafariVacationContext();
      //SELECT * FROM SeenAnimals
      var animals = db.SeenAnimals.OrderBy(seenanimals => seenanimals.Species);
      //return the results
      return animals.ToList();
    }

    [HttpGet("place/{location}")]
    public ActionResult<IEnumerable<SeenAnimals>> GetAction2([FromRoute]string location)
    {
      // query my database
      var db = new SafariVacationContext();
      //SELECT * FROM SeenAnimals
      var animals = db.SeenAnimals.Where(w => w.LocationOfLastSeen.ToLower() == location).OrderBy(seenanimals => seenanimals.LocationOfLastSeen);
      //return the results
      return animals.ToList();
    }

    // // GET /api/aniamls/totalcount/lions
    // [HttpGet("totalcount/{specie}")]
    // public ActionResult<int> GetActionResults([FromRoute] string species){

    // }

    ////// CountOfTimesSeen 
    [HttpGet("totalcount")]
    public ActionResult<int> GetAction3()
    {
      // query my database
      var db = new SafariVacationContext();
      //SELECT * FROM SeenAnimals
      var animals = db.SeenAnimals.Sum(seenanimal => seenanimal.CountOfTimesSeen);
      //return the results
      return animals;
    }
    ///////
    [HttpPost]
    public ActionResult<SeenAnimals> AddAnimals([FromBody] SeenAnimals incomingSeenAnimals)
    {
      var db = new SafariVacationContext();
      db.SeenAnimals.Add(incomingSeenAnimals);
      db.SaveChanges();
      return incomingSeenAnimals;
    }
    [HttpDelete("{id}")]
    public ActionResult<Object> DeleteSeenAnimals([FromRoute]int id)
    {
      Console.WriteLine($"Delete the seenanimal: {id}");

      var db = new SafariVacationContext();
      var seenanimalsToDelete = db.SeenAnimals.FirstOrDefault(seenanimals => seenanimals.Id == id);
      if (seenanimalsToDelete != null)
      {
        db.SeenAnimals.Remove(seenanimalsToDelete);
        db.SaveChanges();
        return seenanimalsToDelete;
      }
      else
      {
        return new { message = "Animal not found" };
      }

    }

    [HttpDelete("location/{location}")]
    public ActionResult<Object> DeleteLocatedAnimals([FromRoute]string location)
    {
      Console.WriteLine($"Delete the seenanimal: {location}");

      var db = new SafariVacationContext();
      var locatedAnimalsToDelete = db.SeenAnimals.FirstOrDefault(seenanimals => seenanimals.LocationOfLastSeen.ToLower() == location);
      if (locatedAnimalsToDelete != null)
      {
        db.SeenAnimals.Remove(locatedAnimalsToDelete);
        db.SaveChanges();
        return locatedAnimalsToDelete;
      }
      else
      {
        return new { message = "Animal not found" };
      }

    }

    [HttpPut("{id}")]

    public ActionResult<object> UpdateSeenAnimals([FromRoute]int id, [FromBody]SeenAnimals newInformation)
    {
      var db = new SafariVacationContext();

      var seenanimal = db.SeenAnimals.FirstOrDefault(animal => animal.Id == id);
      if (seenanimal != null)
      {
        seenanimal.Species = newInformation.Species;
        seenanimal.CountOfTimesSeen = newInformation.CountOfTimesSeen;
        seenanimal.LocationOfLastSeen = newInformation.LocationOfLastSeen;


        db.SaveChanges();
        return Ok(seenanimal);
      }
      else
      {
        return NotFound();
      }

    }
  }
}