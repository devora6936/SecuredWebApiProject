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
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;
        IMapper _mapper;
        public CategoriesController(ICategoryService categoryService ,IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        
        // GET: api/<AuthController>
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            List<Category> categories = await _categoryService.Get();
            
            if (categories != null)
            {
                List<CategoryDTO> categoriesDTO = _mapper.Map<List<Category>, List<CategoryDTO>>(categories);
                return Ok(categoriesDTO) ;
            }
                
            return NoContent();
        }
    }
}
