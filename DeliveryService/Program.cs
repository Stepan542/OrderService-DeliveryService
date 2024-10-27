using DeliveryService.Data;
using DeliveryService.Interfaces;
using DeliveryService.Repositories;
using DeliveryService.Services;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using DeliveryService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DeliveryDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DeliveryDb")));

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryService, DeliveryServiceOrder>();

builder.Services.AddMassTransit(x =>
{
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
        });
    });
}
);

builder.Services.AddScoped<OrderForDeliveryConsumer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
