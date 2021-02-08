using System;
using System.Collections.Generic;

namespace ProductCatalog.Domain.Models
{
    public class Report
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public List<Movement> Purchases { get; set; }

        public List<Movement> Sales { get; set; }
    }

    public  class Movement
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }
    }
}
