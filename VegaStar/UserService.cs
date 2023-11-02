using System;
using VegaStar.Entity;
namespace VegaStar

{
	public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(r => r.UserId == id);
            if (user == null)
            {
                return null; // или можете вернуть, например, User.NotFound, если у вас есть такой статический метод
            }
            return user;

        }

    }
}

