using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Interfaces.Repositories;
using ProductCatalog.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProductCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        // GET: api/<ReportController>
        [HttpGet]
        public ActionResult<IEnumerable<Report>> Get()
        {
            try
            {
                return Ok(_reportRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }
    }
}
