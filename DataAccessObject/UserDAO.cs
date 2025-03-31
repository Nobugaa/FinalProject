using BusinessObject.Models;
using Repository;

namespace DataAccessObject
{
    public class UserDAO
    {
        private readonly IUserRepository _userRepository;
        public UserDAO(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User FindByEmail(string email) => _userRepository.FindByEmail(email);
        public bool ValidateUser(string emaill, string password)
        {
            var user = FindByEmail(emaill);
            if (user == null || user.Active != true)
            {
                return false;
            }
            return user.Password == password;
        }
    }
}
