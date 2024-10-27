using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;

namespace DeliveryService.Interfaces
{
    public interface IDeliveryService
    {
        Task CreateDeliveryAsync(Order order);
    }
}