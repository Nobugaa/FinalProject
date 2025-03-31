using BusinessObject.Models;

namespace Repository
{
    public interface IBorrowRepository : IRepository<BorrowRecord>
    {
        bool UserHasBorrowed(int userId, int bookId);
        BorrowRecord GetActiveBorrowRecord(int userId, int bookId);
    }
}
