using System.Threading.Tasks;
using IdentityTask.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityTask.Services
{
    public interface IUserService
    { 
        /// <summary>
        /// Method for register new users 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IdentityResult> Register(RegisterModel model);

        /// <summary>
        /// Method for login new users
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns token</returns>
        Task<string> LoginToken(LoginModel model);

        /// <summary>
        /// Method return info about user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<UserInfoModel> UserInfo(string userName);
    }
}
