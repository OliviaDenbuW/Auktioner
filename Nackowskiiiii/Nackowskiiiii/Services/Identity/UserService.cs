using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Nackowskiiiii.Data;
using Nackowskiiiii.DataLayer;
using Nackowskiiiii.Models;
using Nackowskiiiii.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;
        private UserManager<ApplicationUser> _userManager;

        public UserService(IHttpContextAccessor httpContextAccessor,
                           IUserRepository userRepository,
                           UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public string GetCurrentUserName()
        {
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            return userName;
        }

        public string AddNewAdmin(AdminViewModel viewModel)
        {
            bool userNameIsUnique = NewAdminIsValid(viewModel.Email);

            if (userNameIsUnique == true)
            {
                ApplicationUser newAdmin = new ApplicationUser();
                newAdmin.UserName = viewModel.Email;
                newAdmin.Email = viewModel.Email;

                IdentityResult result = _userManager.CreateAsync(newAdmin, viewModel.Password).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(newAdmin, "Admin").Wait();
                }

                return "Succeeded"/*result.ToString()*/;
            }

            return "Failed";
        }

        public bool NewAdminIsValid(string newUsername)
        {
            bool usernameIsUnique = false;

            if (_userManager.FindByNameAsync(newUsername).Result == null)
            {
                usernameIsUnique = true;
            }
            else
            {
                usernameIsUnique = false;
            }
            
            return usernameIsUnique;
        }
    }
}
