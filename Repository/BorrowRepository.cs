using BusinessObject.Models;

namespace Repository
{
    public class BorrowRepository : Repository<BorrowRecord>, IBorrowRepository
    {
        public BorrowRepository(MyLibraryContext context) : base(context)
        {

        }

        public bool UserHasBorrowed(int userId, int bookId)
        {
            return _context.BorrowRecords.Any(e => e.UserId == userId && e.BookId == bookId && !e.Returned);
        }

        public BorrowRecord GetActiveBorrowRecord(int userId, int bookId)
        {
            return _context.BorrowRecords.FirstOrDefault(e => e.UserId == userId && e.BookId == bookId && !e.Returned);
        }
    }
}
