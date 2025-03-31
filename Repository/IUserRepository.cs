using BusinessObject.Models;

namespace Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByEmail(string email);
    }
}
