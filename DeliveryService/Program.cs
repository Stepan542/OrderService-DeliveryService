using DeliveryService.Data;
using DeliveryService.Interfaces;
using DeliveryService.Repositories;
using DeliveryService.Services;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using DeliveryService.Consumers;
using DeliveryService.Mappers;
using DeliveryService.Configurations;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DeliveryDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DeliveryDb")));

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryService, DeliveryServiceOrder>();

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderForDeliveryConsumer>(config => 
    {
        config.UseMessageRetry(retryConfig => 
        {
            retryConfig.Interval(3, TimeSpan.FromSeconds(5));
        });
    });

    x.UsingRabbitMq((context, cfg) => // вытаскивать из appsettings (Options)
    {
        var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
        
        // не работает
        // cfg.Host(options.Host, options.Port, h =>
        // {
        //     h.Username(options.Username);
        //     h.Password(options.Password);
        // }); 

        // Не могу добавить port
        cfg.Host(options.Host, h =>
        {
            h.Username(options.Username);
            h.Password(options.Password);
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
builder.Services.AddAutoMapper(typeof(DeliveryMappingProfile));

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
