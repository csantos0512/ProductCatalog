using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProductCatalog.Domain.DataTransferObjects;
using ProductCatalog.Domain.Interfaces.Repositories;
using ProductCatalog.Domain.Models;
using ProductCatalog.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_productRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var result = _productRepository.GetById(id);

                if (result != null)
                    return result;
                else
                    return NotFound(new { Mensagem = "Product not found!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            try
            {
                if (_productRepository.Add(product))
                    return Ok(new
                    {
                        Message = "Product successfully inserted!"
                    });
                else
                    throw new Exception("Error adding product!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut]
        public ActionResult Put(Product product)
        {
            try
            {
                if (_productRepository.Update(product))
                    return Ok(new { Message = "Product successfully updated!" });
                else
                    return NotFound(new { Mensagem = "Product not found!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("updatePricesByCategoryId")]
        public ActionResult Put(PriceUpdateDTO priceUpdate)
        {
            try
            {
                if (_productRepository.UpdatePricesByCategoryId(priceUpdate))
                    return Ok(new { Message = "Products prices successfully updated!" });
                else
                    return NotFound(new { Mensagem = "Product not found!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete]
        public ActionResult Delete(Product product)
        {
            try
            {
                if (_productRepository.Remove(product))
                    return Ok(new { Message = "Product successfully deleted!" });
                else
                    return NotFound(new { Mensagem = "Product not found!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
