using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderServiceAppAPI.Dto.Order;
using Shared.Models;
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
        public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
        {
            var orders = await _orderService.GetAllAsync();
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Order order)
        {
            var createdOrder = await _orderService.CreateAsync(order);

            // та самая очередь, которая ничего не делает

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

            // если async будет в конце, возможна ошибка:
            // System.InvalidOperationException: No route matches the supplied values.
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id, [FromBody] Order order)
        {
            if (id != order.Id) return BadRequest();
            await _orderService.UpdateAsync(order);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _orderService.DeleteAsync(id);

            return NoContent();
        }
    }   
}