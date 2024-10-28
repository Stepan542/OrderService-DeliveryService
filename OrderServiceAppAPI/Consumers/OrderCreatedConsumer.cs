using MassTransit;
using OrderServiceAppAPI.Interfaces;

namespace OrderServiceAppAPI.Consumers
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        // просто ставит в очередь, передавая пустой интерфейс который не наследует модели
        public async Task Consume(ConsumeContext<IOrderCreated> context)
        {
            var message = context.Message;
            Console.WriteLine($"Order Created: {message.Id} at {message.OrderDate}");

            await Task.CompletedTask;
        }
    }
}