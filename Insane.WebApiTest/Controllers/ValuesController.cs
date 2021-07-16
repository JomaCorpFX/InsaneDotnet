using Insane.AspNet.Identity.Model1.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Insane.WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IdentityDbContextBase context;

        public ValuesController(IdentityDbContextBase context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public string Get()
        {
            return $"Instance of DbContext created for this request: {context.GetType().FullName}";
        }

       
    }
}
