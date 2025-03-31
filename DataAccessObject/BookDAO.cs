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
            var exist = _bookRepository.GetById(book.Id);
            if (exist == null)
            {
                _bookRepository.Add(book);
                return true;
            }
            return false;

        }
        public bool Delete(Book book)
        {
            var exist = _bookRepository.GetById(book.Id);
            if (exist != null)
            {
                _bookRepository.Delete(book.Id);
                return true;
            }
            return false;
        }
        public Book GetById(int id)
        {
            return _bookRepository.GetById(id);
        }
        public bool Update(Book book)
        {
            var exist = _bookRepository.GetById(book.Id);
            if (exist == null)
            {
                _bookRepository.Update(book);
                return true;
            }
            return false;
        }
    }
}
