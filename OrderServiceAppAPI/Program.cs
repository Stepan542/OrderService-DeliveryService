using Microsoft.EntityFrameworkCore;
using OrderServiceAppAPI.Data;
using OrderServiceAppAPI.Repositories;
using OrderServiceAppAPI.Services;
using Microsoft.OpenApi.Models;
//using Ocelot.DependencyInjection;
using MassTransit;
using OrderServiceAppAPI.Mappers;
using OrderServiceAppAPI.Interfaces;
using OrderServiceAppAPI.Configurations;
using MassTransit.Configuration;
// using OrderServiceAppAPI.Consumers;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});


builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));
// вынести в app settings
builder.Services.AddMassTransit(x =>
{
    // пустая очередь
    // x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        // cfg.Host("localhost", 5672, "/", h => 
        // {
        //     h.Username("guest");
        //     h.Password("guest");
        // });

        var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

        cfg.Host(options.Host, h =>
        {
            h.Username(options.Username);
            h.Password(options.Password);

        });

        // пустая очередь

        // cfg.ReceiveEndpoint("order-created-queue", e => 
        // {
        //     e.ConfigureConsumer<OrderCreatedConsumer>(context);
        // });
    });
});
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Order Service API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ocelot API Gateway v1");
});
app.MapControllers();

//app.UseRouting();
//app.UseHttpsRedirection();
//app.UseAuthorization();

app.Run();

