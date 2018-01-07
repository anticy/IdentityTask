using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using IdentityTask.Authentication;
using IdentityTask.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityTask.Services
{
    public class UserService : IUserService
    {
       
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private IToken _token;

        public UserService(UserManager<User> userManager, SignInManager<User> signManager, IToken token)
        {
            _userManager = userManager;
            _signManager = signManager;
            _token = token;
        }

        /// <summary>
        /// Method for register new users 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Register(RegisterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Age = model.Age,
            };

            return await _userManager.CreateAsync(user, model.Password);
        }
        /// <summary>
        /// Method for login new users
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns token</returns>
        public async Task<string> LoginToken(LoginModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) throw new AuthenticationException($"User {model.Email} user does not exist");
            var result = await _signManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            return result.Succeeded ? _token.GenerateJwt(user) : throw new AuthenticationException("Error during authorization");
        }
        /// <summary>
        /// Method return info about user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<UserInfoModel> UserInfo(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            var user = await _userManager.FindByEmailAsync(userName);
            if (user == null) throw new AuthenticationException($"User {userName} user does not exist");

            var userInfo = new UserInfoModel
            {
                Age = user.Age,
                Country = user.Country,
                Email = user.Email                
            };
            return userInfo;

        }
    }
}
