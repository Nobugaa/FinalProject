using BusinessObject.Models;

namespace Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyLibraryContext context) : base(context)
        {
        }

        public User FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
