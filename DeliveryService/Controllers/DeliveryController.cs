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

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery([FromBody]Order order)
        {
            await _deliveryService.CreateDeliveryAsync(order);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryById(int id)
        {
            Console.WriteLine($"got id {id}");
            var delivery = await _deliveryService.GetDeliveryByIdAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }
    }
}