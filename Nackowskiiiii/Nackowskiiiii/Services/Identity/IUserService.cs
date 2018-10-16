using Nackowskiiiii.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Services.Identity
{
    public interface IUserService
    {
        string GetCurrentUserName();

        string AddNewAdmin(AdminViewModel newAdmin);

        bool NewAdminIsValid(string newUsername);
    }
}
