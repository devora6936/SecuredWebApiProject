using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstApiSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService userService, IMapper mapper , ILogger<UsersController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }
        

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            User user =await _userService.GetUserById(id);
            if (user != null)
            {
                UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
                return Ok(userDTO);
            }
               
            return BadRequest();
        }


        // POST api/<AuthController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDTO user)
        {
            int goodPass = _userService.CheckPass(user.Password);
            if (goodPass<=2)
                return BadRequest();
            User userFRomDTO = _mapper.Map<UserDTO, User>(user);
            User newUser =await _userService.AddUser(userFRomDTO);
            return CreatedAtAction(nameof(Get), new { id = newUser.UserId }, newUser);

        }

        // POST api/<AuthController>
        [HttpPost]

        [Route("login")]
        public async Task<ActionResult<UserDTO>> login([FromBody] UserLoginDTO u)
        {
           
            User user = _mapper.Map<UserLoginDTO, User>(u);
            _logger.LogInformation($"Loggin attemped with user name,{user.Email} and password {user.Password}");
            User newUser =await _userService.login(user);
            UserDTO userDTO = _mapper.Map<User, UserDTO>(newUser);

            if (user != null)
            {
                Response.Cookies.Append("X-Access-Token", user.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                return Ok(userDTO);
            }
                
            return Unauthorized();

        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UserDTO u)
        {
            int goodPass = _userService.CheckPass(u.Password);
            if (goodPass<=2)
                return BadRequest();
            User userFRomDTO = _mapper.Map<UserDTO, User >(u);

            User user =await _userService.UpdateUser(userFRomDTO, id);
            if (user != null)
            {
                UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
                return Ok(userDTO);


            }
            return BadRequest();

        }

        // POST api/<AuthController>
        [HttpPost]
        [Route("checkPassword")]
        public int checkPassword([FromBody] String password)
        {
            int goodPass = _userService.CheckPass(password);
            return goodPass;
 
        }

    }
}
