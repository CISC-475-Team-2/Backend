using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SeatingChartAPI.Controllers
{
    [Route("api/[controller]")]
    public class SeatingChartController : ApiController
    {
        [HttpGet()]
        public IHttpActionResult GetSeatingChart()
        {
            return Ok(System.IO.File.ReadAllText("/App_Data/SeatingJSON.txt"));
        }
    }
}
