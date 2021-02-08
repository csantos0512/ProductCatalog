using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Domain.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public int OrderTypeId { get; set; }

        public decimal OrderTotal { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
