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
        public IHttpActionResult Get()
        {
            JSONBuilder jb = new JSONBuilder();
            Dictionary<string, Dictionary<string, string>> data = jb.loadData();
            jb.handleNoise(data);
            jb.writeDictionaryToFile(data);
            return Ok(System.IO.File.ReadAllText(@"C:\Users\Public\App_Data\seatingChartJSON.txt"));
        }

    }
}
