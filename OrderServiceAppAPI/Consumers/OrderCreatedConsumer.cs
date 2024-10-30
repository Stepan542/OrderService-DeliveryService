using MassTransit;
using OrderServiceAppAPI.Interfaces;

namespace OrderServiceAppAPI.Consumers
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        // Rabbit котоырый просто создаёт очередь, но ничего не делает.
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IOrderCreated> context)
        {
            var message = context.Message;
            _logger.LogInformation($"Order Created: {message.Id} at {message.OrderDate}");

            await Task.CompletedTask;
        }
    }
}