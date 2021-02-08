using ProductCatalog.Domain.Models;
using System.Collections.Generic;

namespace ProductCatalog.Domain.Interfaces.Repositories
{
    public interface IReportRepository
    {
        IEnumerable<Report> GetAll();
    }
}
