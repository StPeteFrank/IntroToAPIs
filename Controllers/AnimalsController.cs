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
  }
}