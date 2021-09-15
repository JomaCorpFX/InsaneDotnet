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
        private readonly IdentityDbContextBase ctx;
        private readonly IdentitySqlServerDbContext context;
        private readonly IdentityPostgreSqlDbContext context2;

        public ValuesController(IdentityDbContextBase ctx /*IdentitySqlServerDbContext context, IdentityPostgreSqlDbContext context2*/)
        {
            this.ctx = ctx;
            //this.context = context;
            //this.context2 = context2;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public object Get()
        {
            //var z = context.Database.ProviderName;
            //return $"Instance of DbContext created for this request: {context.GetType().FullName} / {context2.GetType().FullName}" ;
            return new { Message = $"Instance of DbContext created for this request: {ctx.GetType().FullName} - Provider: {ctx.Database.ProviderName}",
            Roles = ctx.Roles.ToList()
            };
        }


    }
}
