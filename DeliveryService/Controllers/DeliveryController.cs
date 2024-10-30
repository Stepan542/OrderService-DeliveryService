using DeliveryService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly ILogger<DeliveryController> _logger;

        public DeliveryController(IDeliveryService deliveryService, ILogger<DeliveryController> logger)
        {
            _deliveryService = deliveryService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Order order)
        {
            await _deliveryService.CreateAsync(order);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation($"got id {id}");
            var delivery = await _deliveryService.GetByIdAsync(id);

            if (delivery == null) return NotFound();

            return Ok(delivery);
        }
    }
}