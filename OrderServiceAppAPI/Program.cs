using Microsoft.EntityFrameworkCore;
using OrderServiceAppAPI.Data;
using OrderServiceAppAPI.Repositories;
using OrderServiceAppAPI.Services;
using Microsoft.OpenApi.Models;
//using Ocelot.DependencyInjection;
using MassTransit;
using OrderServiceAppAPI.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    // x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5672, "/", h => 
        {
            h.Username("guest");
            h.Password("guest");
        });

        // cfg.ReceiveEndpoint("order-created-queue", e => 
        // {
        //     e.ConfigureConsumer<OrderCreatedConsumer>(context);
        // });
    });
});

builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Order Service API", Version = "v1" });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ocelot API Gateway v1");
});

//app.UseRouting();
//app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();

