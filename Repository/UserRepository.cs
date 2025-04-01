using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyLibraryContext context) : base(context)
        {
        }
        public User GetByIdWithBorrowRecords(int id)
        {

            return _context.Users
                .Include(u => u.BorrowRecords)
                    .ThenInclude(br => br.Book) // Load books in borrow records
                .FirstOrDefault(u => u.Id == id);

        }



        public User FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }


    }
}
