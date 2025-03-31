using BusinessObject.Models;
using Repository;

namespace DataAccessObject
{
    public class BookDAO
    {
        private readonly IBookRepository _bookRepository;
        public BookDAO(IBookRepository bookRepository) { _bookRepository = bookRepository; }
        public IEnumerable<Book> GetAll() => _bookRepository.GetAll();
        public IEnumerable<Book> FindByName(string name)
        {
            var books = _bookRepository.GetAll();
            if (string.IsNullOrEmpty(name))
            {
                return books;
            }
            name = name.ToLower();
            return books.Where(c => c.Title.ToLower().Contains(name));
        }
        public bool Create(Book book)
        {
            var exist = _bookRepository.FindByName(book.Title);
            if (exist == null)
            {
                _bookRepository.Add(book);
                return true;
            }
            return false;

        }
    }
}
