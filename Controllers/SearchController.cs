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
  public class SearchController : ControllerBase
  {
    [HttpGet]
    public ActionResult<IEnumerable<SeenAnimals>> Search([FromQuery]string name)
    {
      var db = new SafariVacationContext();
      var results = db.SeenAnimals.Where(seenanimals => seenanimals.Species.ToLower().Contains(name.ToLower()));
      return results.ToList();
    }
  }
}