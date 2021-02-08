using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using ProductCatalog.Domain.Interfaces.Repositories;
using ProductCatalog.Domain.Models;
using ProductCatalog.Infra.Data.SQLScripts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using ProductCatalog.Domain.DataTransferObjects;

namespace ProductCatalog.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProductCatalog");
        }

        public bool Add(Product obj)
        {
            bool result = false;

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@ProductName", obj.ProductName);
            queryParameters.Add("@CategoryId", obj.CategoryId);
            queryParameters.Add("@Price", obj.Price);
            queryParameters.Add("@Stock", obj.Stock);

            using SqlConnection connection = new SqlConnection(
                _connectionString);
            int affectedRows = connection.Execute(ProductScripts.INSERT_PRODUCT, queryParameters);

            if (affectedRows > 0)
                result = true;

            return result;
        }

        public IEnumerable<Product> GetAll()
        {
            using SqlConnection connection = new SqlConnection(
                _connectionString);

            var products = connection.Query<Product, Category, Product>(ProductScripts.SELECT_ALL_PRODUCTS, (product, category) =>
            {
                product.Category = category;
                return product;
            },
            splitOn: "CategoryId");

            return products;
        }

        public Product GetById(int id)
        {
            using SqlConnection connection = new SqlConnection(
                _connectionString);

            var products = connection.Query<Product, Category, Product>(ProductScripts.SELECT_PRODUCT, (product, category) =>
            {
                product.Category = category;
                return product;
            },
            param: new { ProductId = id },
            splitOn: "CategoryId").FirstOrDefault();

            return products;
        }

        public bool Remove(Product obj)
        {
            bool result = false;

            using SqlConnection connection = new SqlConnection(
                _connectionString);

            int affectedRows = connection.Execute(ProductScripts.DELETE_PRODUCT, new { obj.ProductId });

            if(affectedRows > 0)
                result = true;
            
            return result;
        }

        public bool Update(Product obj)
        {
            bool result = false;

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@ProductId", obj.ProductId);
            queryParameters.Add("@ProductName", obj.ProductName);
            queryParameters.Add("@CategoryId", obj.CategoryId);
            queryParameters.Add("@Price", obj.Price);
            queryParameters.Add("@Stock", obj.Stock);

            using SqlConnection connection = new SqlConnection(
                _connectionString);

            int affectedRows = connection.Execute(ProductScripts.UPDATE_PRODUCT, queryParameters);

            if (affectedRows > 0)
                result = true;

            return result;
        }

        public bool UpdatePricesByCategoryId(PriceUpdateDTO priceUpdate)
        {
            bool result = false;

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@CategoryId", priceUpdate.CategoryId);
            queryParameters.Add("@PercentageValue", (double)priceUpdate.PercentageValue / 100);
            
            using SqlConnection connection = new SqlConnection(
                _connectionString);

            int affectedRows = 0;

            switch (priceUpdate.PriceUpdateType)
            {
                case Domain.Enums.PriceUpdateType.Adjustment:
                    affectedRows = connection.Execute(ProductScripts.INCREASE_PRICES, queryParameters);
                    break;
                case Domain.Enums.PriceUpdateType.Discount:
                    affectedRows = connection.Execute(ProductScripts.REDUCE_PRICES, queryParameters);
                    break;
                default:
                    break;
            }

            if (affectedRows > 0)
                result = true;

            return result;
        }
    }
}
