using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Domain.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal ItemTotal { get; set; }
    }
}
