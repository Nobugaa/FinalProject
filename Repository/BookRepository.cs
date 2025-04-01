using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(MyLibraryContext context) : base(context) { }
        public Book FindByName(string name)
        {
            return _dbSet.FirstOrDefault(c => c.Title == name);
        }
        public Book GetByIdWithBorrowRecords(int id)
        {


            return _context.Books
                .Include(b => b.BorrowRecords)
                    .ThenInclude(br => br.User) // Load the user who borrowed the book
                .Include(b => b.AddedByNavigation) // Load the user who added the book
                .FirstOrDefault(b => b.Id == id);

        }
        public List<Book> GetBooksAddedByUser(int userId)
        {
            return _context.Books
                .Where(b => b.AddedBy == userId)
                .ToList();
        }
    }

}
