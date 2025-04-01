
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
        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public User GetByIdWithBorrowRecords(int id)
        {
            return _userRepository.GetByIdWithBorrowRecords(id);
        }
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public bool Update(User user)
        {
            var exist = _userRepository.GetById(user.Id);
            if (exist != null)
            {
                _userRepository.Update(user);
                return true;
            }
            return false;
        }
        public void Add(User user)
        {
            _userRepository.Add(user);
        }
    }
}
