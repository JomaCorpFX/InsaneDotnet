using Insane.AspNet.Identity.Model1;
using Insane.AspNet.Identity.Model1.Context;
using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFrameworkCore;
using Insane.Extensions;
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



