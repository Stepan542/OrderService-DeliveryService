using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.OpenApi.Models;
//using MMLib.SwaggerForOcelot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Ocelot Gateway API", Version = "v1" });
});

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddOcelot();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerForOcelotUI(options => {
    options.PathToSwaggerGenerator = "/swagger/docs";
});

await app.UseOcelot();

app.Run();