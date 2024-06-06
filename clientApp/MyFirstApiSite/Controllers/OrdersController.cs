using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstApiSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;
        IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        
        // POST api/<AuthController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] OrderDTO orderDTO)
        {
            Order Order= _mapper.Map<OrderDTO, Order>(orderDTO);

            Order newOrder =await _orderService.AddOrder(Order);
            if (newOrder != null)
            { 
                OrderDTO orderToReturn = _mapper.Map<Order, OrderDTO> (newOrder);
                return Ok(orderToReturn);
            }
            return BadRequest();
        }

    }
}
