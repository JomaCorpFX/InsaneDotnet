using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insane.WebApiLiveTest
{
public class MyContext : DbContext
{
    /* 
        * This is an example class
        Your specific DbSets Here
        */

    public MyContext(DbContextOptions options) : base(options)
    {
    }
}
}
