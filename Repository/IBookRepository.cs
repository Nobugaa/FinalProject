using BusinessObject.Models;

namespace Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        Book FindByName(string name);
        Book GetByIdWithBorrowRecords(int id);

        List<Book> GetBooksAddedByUser(int userId);

    }
}
