using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> CreateDeliveryAsync([FromBody]Order order)
        {
            await _deliveryService.CreateDeliveryAsync(order);
            return Ok();
        }
    }
}