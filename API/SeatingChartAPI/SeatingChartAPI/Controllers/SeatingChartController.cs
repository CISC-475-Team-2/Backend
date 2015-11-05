using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SeatingChartAPI.Controllers
{
    public class SeatingChartController : ApiController
    {
        public IHttpActionResult GetProduct()
        {
            return Ok(System.IO.File.ReadAllText("/App_Data/SeatingJSON.txt"));
        }
    }
}
