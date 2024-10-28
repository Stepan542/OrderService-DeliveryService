using DeliveryService.Data;
using DeliveryService.Interfaces;
using DeliveryService.Repositories;
using DeliveryService.Services;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using DeliveryService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DeliveryDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DeliveryDb")));

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryService, DeliveryServiceOrder>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderForDeliveryConsumer>(config => 
    {
        config.UseMessageRetry(retryConfig => 
        {
            retryConfig.Interval(3, TimeSpan.FromSeconds(5));
        });
    });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5672, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order-delivery-last-try", e =>
        {
            e.Consumer<OrderForDeliveryConsumer>(context);
            // e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        });
    });
}
);

// builder.Services.AddScoped<OrderForDeliveryConsumer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Delivery Service");
    });
}

// app.UseHttpsRedirection();


app.Run();
