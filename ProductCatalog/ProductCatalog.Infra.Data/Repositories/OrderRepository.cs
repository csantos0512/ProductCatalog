using Dapper;
using Microsoft.Extensions.Configuration;
using ProductCatalog.Domain.Interfaces.Repositories;
using ProductCatalog.Domain.Models;
using ProductCatalog.Infra.Data.SQLScripts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace ProductCatalog.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProductCatalog");
        }

        public bool Add(Order obj)
        {
            bool result = false;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var queryParameters = new DynamicParameters();
                    queryParameters.Add("@OrderDate", obj.OrderDate);
                    queryParameters.Add("@OrderTypeId", obj.OrderTypeId);
                    queryParameters.Add("@OrderTotal", obj.OrderTotal);
                    
                    try
                    {
                        obj.OrderId = connection.QuerySingle<int>(OrderScripts.INSERT_ORDER, queryParameters, transaction: transaction);

                        if (obj.OrderId > 0)
                        {
                            foreach (var item in obj.OrderItems)
                            {
                                queryParameters = new DynamicParameters();
                                queryParameters.Add("@ItemTotal", item.ItemTotal);
                                queryParameters.Add("@OrderId", obj.OrderId);
                                queryParameters.Add("@ProductId", item.ProductId);
                                queryParameters.Add("@Quantity", item.Quantity);
                                queryParameters.Add("@UnitPrice", item.UnitPrice);

                                var affectedRows = connection.Execute(OrderScripts.INSERT_ORDER_ITEM, queryParameters, transaction: transaction);

                                if(affectedRows == 0)
                                throw new Exception("Error adding order!");
                                else
                                {
                                    var product = connection.Query<Product>(ProductScripts.SELECT_PRODUCT, new { item.ProductId }, transaction: transaction).FirstOrDefault();

                                    if(product == null)
                                        throw new Exception("Error adding order!");

                                    if(obj.OrderTypeId == 1)
                                        product.Stock += item.Quantity;
                                    else
                                        product.Stock -= item.Quantity;

                                    queryParameters = new DynamicParameters();
                                    queryParameters.Add("@ProductId", product.ProductId);
                                    queryParameters.Add("@ProductName", product.ProductName);
                                    queryParameters.Add("@CategoryId", product.CategoryId);
                                    queryParameters.Add("@Price", product.Price);
                                    queryParameters.Add("@Stock", product.Stock);

                                    affectedRows = connection.Execute(ProductScripts.UPDATE_PRODUCT, queryParameters, transaction: transaction);
                                }
                            }

                            result = true;

                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        return result;
                    }
                }
            }

            return result;
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Order obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}
