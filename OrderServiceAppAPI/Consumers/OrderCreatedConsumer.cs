using MassTransit;
using OrderServiceAppAPI.Interfaces;

namespace OrderServiceAppAPI.Consumers
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        public async Task Consume(ConsumeContext<IOrderCreated> context)
        {
            var message = context.Message;
            Console.WriteLine($"Order Created: {message.Id} at {message.OrderDate}");

            await Task.CompletedTask;
        }
    }
}