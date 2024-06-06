using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using System.Collections.Generic;
using AutoMapper;
using DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstApiSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        
        // GET: api/<AuthController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery] int? position, [FromQuery] int? skip, [FromQuery] string? desc, [FromQuery] double? minPrice, [FromQuery] double? maxPrice,[FromQuery] int?[] categoryIds)
        {
            
            List<Product> products = await _productService.Get(position,skip,desc,minPrice,maxPrice,categoryIds);
            if (products != null) 
            {
                List<ProductDTO> productsDTO = _mapper.Map<List<Product>, List<ProductDTO>>(products);
                return Ok(productsDTO) ;
            }
            return NoContent();
        }
    }
}
