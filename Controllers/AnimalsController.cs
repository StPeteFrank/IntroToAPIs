using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroToAPIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntroToAPIs.Controllers
{
  [Route("api/[controller]")]
  // localhost:5000/api/values
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
      var results = db.SeenAnimals.OrderBy(seenanimals => seenanimals.Species);
      //return the results
      return results.ToList();
    }
  }
}