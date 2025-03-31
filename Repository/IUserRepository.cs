using BusinessObject.Models;

namespace Repository
{
    public interface IUserRepository
    {
        User FindByEmail(string email);
    }
}
