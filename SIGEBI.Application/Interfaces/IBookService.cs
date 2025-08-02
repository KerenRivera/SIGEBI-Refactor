using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.Interfaces
{
    //usamos esta abstraccion para desacoplar el controlador del servicioo, cumplimos DIP
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(int id);
    }
}
