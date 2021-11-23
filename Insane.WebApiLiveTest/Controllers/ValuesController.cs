
using Insane.WebApiLiveTest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    private readonly MyContext ctx;

    public ValuesController(MyContext ctx)
    {
        this.ctx = ctx;
    }

    // GET: api/<ValuesController>
    [HttpGet]
    public object Get()
    {
        return new
        {
            Instance = $"{ctx.GetType().Name}",
            Provider = $"{ctx.Database.ProviderName}",
            ConnectionString = $"{ctx.Database.GetDbConnection().ConnectionString}"
        };
    }


}
}
