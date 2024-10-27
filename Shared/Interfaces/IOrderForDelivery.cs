using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IOrderForDelivery
    {
        int Id { get; set; }
        string Name { get; set; }
        int Quantity { get; set; }
        decimal Price { get; set; }
        DateTime OrderDate { get; set; }
    }
}