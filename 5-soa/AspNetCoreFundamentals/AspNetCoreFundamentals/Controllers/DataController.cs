using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFundamentals.Controllers
{
    [Route("[controller]")] // uses the current class's name as a variable of sorts. same as [Route("data")]
    public class DataController
    {
        //[HttpGet("1")] // GET data/1
        [HttpGet] // GET data
        public string GetData()
        {
            return "data";
        }
    }
}
