using SIGEBI.Web.Models;
namespace SIGEBI.Web.Contracts
{
    public interface IBookRepository 
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task<bool> UpdateAsync(BookModel book);
        Task<bool> CreateAsync(BookModel book);
        Task<bool> DeleteAsync(int id);
    }
}
