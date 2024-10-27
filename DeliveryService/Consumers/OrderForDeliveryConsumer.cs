using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryService.Interfaces;
using MassTransit;
using Shared.Interfaces;
using Shared.Models;

namespace DeliveryService.Consumers
{
    public class OrderForDeliveryConsumer : IConsumer<IOrderForDelivery>
    {
        private readonly IDeliveryService _deliveryService;

        public OrderForDeliveryConsumer(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
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

            await _deliveryService.CreateDeliveryAsync(deliveryOrder);
        }
    }
}