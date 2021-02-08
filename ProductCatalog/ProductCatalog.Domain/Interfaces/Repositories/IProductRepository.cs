using ProductCatalog.Domain.DataTransferObjects;
using ProductCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        bool UpdatePricesByCategoryId(PriceUpdateDTO priceUpdate);
    }
}
