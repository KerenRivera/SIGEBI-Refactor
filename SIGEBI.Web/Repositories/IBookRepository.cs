using SIGEBI.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGEBI.Web.Repositories
{
    //la responsabilidad de este repositorio es acceder a los datos de los libros
    public interface IBookRepository 
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task<bool> UpdateAsync(BookModel book);
        Task<bool> CreateAsync(BookModel book);
        Task<bool> DeleteAsync(int id);
    }
}
