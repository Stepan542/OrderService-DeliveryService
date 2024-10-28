using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderServiceAppAPI.Dto.Order;
using OrderServiceAppAPI.Interfaces;
using Shared.Models;
using OrderServiceAppAPI.Repositories;
using OrderServiceAppAPI.Services;
using Shared.Interfaces;

namespace OrderServiceAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, IPublishEndpoint publishEndpoint, 
            ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            
            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder([FromRoute] int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            var createdOrder = await _orderService.CreateOrderAsync(order);

            // await _publishEndpoint.Publish<IOrderCreated>(new 
            // {
            //     OrderId = createdOrder.Id,
            //     CreatedAt = DateTime.UtcNow
            // });
            try {
                await _publishEndpoint.Publish<IOrderForDelivery>(createdOrder);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "an error occured while sending the event.");
            }

            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute]int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }   
}