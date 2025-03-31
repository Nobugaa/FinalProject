using BusinessObject.Models;
using Repository;

namespace DataAccessObject
{
    public class BorrowDAO
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBookRepository _bookRepository;

        public BorrowDAO(IBorrowRepository borrowRepository, IBookRepository bookRepository)
        {
            _borrowRepository = borrowRepository;
            _bookRepository = bookRepository;
        }

        public bool BorrowBook(int userId, int bookId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book == null || book.Quantity < 0)
            {
                return false;
            }
            if (_borrowRepository.UserHasBorrowed(userId, bookId))
            {
                return false;
            }
            var record = new BorrowRecord
            {
                UserId = userId,
                BookId = bookId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.MaxValue,
                Returned = false
            };
            _borrowRepository.Add(record);
            book.Quantity -= 1;
            _bookRepository.Update(book);
            return true;
        }

        public bool ReturnBook(int userId, int bookId)
        {
            var borrowRecord = _borrowRepository.GetActiveBorrowRecord(userId, bookId);
            if (borrowRecord == null || borrowRecord.Returned)
            {
                return false; // No active borrow record found or the book is already returned
            }

            borrowRecord.Returned = true; // Mark as returned
            _borrowRepository.Update(borrowRecord);

            var book = _bookRepository.GetById(bookId);
            if (book != null)
            {
                book.Quantity += 1; // Increment book quantity
                _bookRepository.Update(book);
            }
            return true;
        }
    }
}
