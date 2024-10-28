using DeliveryService.Interfaces;
using MassTransit;
using Shared.Interfaces;
using Shared.Models;

namespace DeliveryService.Consumers
{
    public class OrderForDeliveryConsumer : IConsumer<IOrderForDelivery>
    {
        private readonly IDeliveryService _deliveryService;
        private readonly ILogger<OrderForDeliveryConsumer> _logger;

        public OrderForDeliveryConsumer(IDeliveryService deliveryService, ILogger<OrderForDeliveryConsumer> logger)
        {
            _deliveryService = deliveryService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IOrderForDelivery> context)
        {
            var deliveryOrder = new Order
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                Quantity = context.Message.Quantity,
                Price = context.Message.Price,
                OrderDate = context.Message.OrderDate
            };

            try {
                await _deliveryService.CreateDeliveryAsync(deliveryOrder);
                _logger.LogInformation("the event was successfully processed.");
            }

            catch(Exception ex) {
                _logger.LogError(ex, "an error occured while POST request");
                throw;
            }
            
        }
    }
}