using BusinessObject.Models;

namespace Repository
{
    class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyLibraryContext context) : base(context) { }
    }
}
