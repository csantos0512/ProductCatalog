using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace ProductCatalog.Domain.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; }
    
        public ICollection<Product> Products { get; set; }
    }
}