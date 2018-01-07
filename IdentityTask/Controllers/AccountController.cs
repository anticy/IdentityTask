using System;
using System.Threading.Tasks;
using IdentityTask.Models;
using IdentityTask.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityTask.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _service;

        public AccountController(IUserService userService)
        {
            _service = userService;
        }
        /// <summary>
        /// Method for register new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// post : api/account/register
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _service.Register(model);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Some error");
            }

        }
        /// <summary>
        /// Method for Login user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // post: api/account/token
        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> Token(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {                    
                    return BadRequest("Invalid username or password");
                }

                var response = new
                {
                    access_token = await _service.LoginToken(model),
                    username = model.Email
                };

                Response.ContentType = "application/json";
                return Ok(JsonConvert.SerializeObject(response,
                    new JsonSerializerSettings {Formatting = Formatting.Indented}));
            }
            catch
            {
                return BadRequest("Some error");
            }
        }
    }
}