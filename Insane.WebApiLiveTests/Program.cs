using Insane.AspNet.Identity.Model1;
using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFrameworkCore;
using Insane.Extensions;
using Insane.WebApiLiveTests.EntityFrameworkCore.Context;
using Insane.WebApiLiveTests.EntityFrameworkCore.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommandLine(new string[] { "Hi:Joma=HelloWorld" });


//var z = builder.Configuration.GetSection("");
//builder.Services.Configure<DbContextSettings>(z,);
//builder.Services.AddIdentity<IdentityUser, IdentityRole>(null).AddEntityFrameworkStores<IdentityDbContext>().AddRoleStore;
string Tag = "Insane";
builder.Services.Configure<IdentityOptions>("", options =>
{
    options.Tag = Tag;
});

builder.Services.Configure<IdentityOptions>("A", options =>
{
    options.Tag = Tag + Tag;
});

builder.Services.Configure<IdentityOptions>("B", options =>
{
    options.Tag = Tag + Tag + Tag;
});

builder.Services.AddOptions<IdentityOptions>("Z").Bind(builder.Configuration.GetSection(nameof(IdentityOptions))).ValidateDataAnnotations();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

string tn = typeof(Program).FullName!;
Type tp = Type.GetType(tn);
var value = builder.Configuration.GetValue<string>("AppName");
var hi = builder.Configuration.GetValue<string>("Hi:Joma");
Console.WriteLine($"AppName{1}");

var settings = builder.Configuration.GetSection(nameof(DbContextSettings)).Get<DbContextSettings>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var options = app.Services.GetService<IOptions<IdentityOptions>>();
var options2 = app.Services.GetRequiredService<IOptionsMonitor<IdentityOptions>>();
IdentityOptions z;
using (var scope = app.Services.CreateScope())
{ 
    var options3 = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<IdentityOptions>>();
    z = options3.Get("Z");
}

var empty = options2.Get("").Tag;
var a = options2.Get("A").Tag;
var b = options2.Get("B").Tag;

var op = new DbContextSettings
{
    SqlServerConnectionString = "Data Source=localhost;Initial Catalog=IdentityDb;User Id=sa;Password=100;"
}
.ConfigureDbContextProviderOptions<IdentitySqlServerDbContext>(null, null).Options;

using IdentitySqlServerDbContext context = new IdentitySqlServerDbContext(op);
using IdentitySqlServerDbContext context2 = new IdentitySqlServerDbContextFactory().CreateDbContext(new string[]{ });

//context.Roles.Add(new IdentityRoleString
//{
//    Name = "User"
//});

context.Roles.ToList().ForEach(role => Console.WriteLine(role.Name));
context.SaveChanges();

// Configure the HTTP request pipeline.
Utils.HelloWorld.Show();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



