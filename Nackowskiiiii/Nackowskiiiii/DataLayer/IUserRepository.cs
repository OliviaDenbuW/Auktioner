using Nackowskiiiii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskiiiii.DataLayer
{
    public interface IUserRepository
    {
        void AddNewAdmin(ApplicationUser newAdmin);
    }
}
