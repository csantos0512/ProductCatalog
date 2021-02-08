using Dapper;
using Microsoft.Extensions.Configuration;
using ProductCatalog.Domain.Interfaces.Repositories;
using ProductCatalog.Domain.Models;
using ProductCatalog.Infra.Data.SQLScripts;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ProductCatalog.Infra.Data.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly string _connectionString;

        public ReportRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ProductCatalog");
        }

        public IEnumerable<Report> GetAll()
        {
            using SqlConnection connection = new SqlConnection(
                _connectionString);

            var reports = connection.Query<Report>(ReportScripts.SELECT_REPORT);

            foreach (var item in reports)
            {
                item.Purchases = connection.Query<Movement>(ReportScripts.SELECT_MOVEMENT, new { item.ProductId, OrderTypeId = 1 }).ToList();
                item.Sales = connection.Query<Movement>(ReportScripts.SELECT_MOVEMENT, new { item.ProductId, OrderTypeId = 2 }).ToList();
            }

            return reports;
        }
    }
}
