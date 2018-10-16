using Nackowskiiiii.Data;
using Nackowskiiiii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskiiiii.DataLayer
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddNewAdmin(ApplicationUser newAdmin)
        {
            _context.Users.Add(newAdmin);
            _context.SaveChanges();
        }
    }
}
