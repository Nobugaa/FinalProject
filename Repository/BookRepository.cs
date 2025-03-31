using BusinessObject.Models;

namespace Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(MyLibraryContext context) : base(context) { }
        public Book FindByName(string name)
        {
            return _dbSet.FirstOrDefault(c => c.Title == name);
        }
    }

}
