using System.Threading.Tasks;
using IdentityTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityTask.Controllers
{
    [Route("api/[controller]")]
    public class InfoController : Controller
    {
        private readonly IUserService _userService;
        public InfoController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Method returns info about user. Only for Authorize users
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = User.Identity.Name;
            var userInfo = await _userService.UserInfo(user);
            return Ok(JsonConvert.SerializeObject(userInfo, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
