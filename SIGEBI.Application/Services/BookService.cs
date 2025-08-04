using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Interfaces;


namespace SIGEBI.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public void AddBook(Book book)
        {
            _bookRepository.Add(book);
        }

        public bool UpdateBook(Book book)
        {
            var existingBook = _bookRepository.GetById(book.Id);
            if (existingBook == null)
                return false; 

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;

            _bookRepository.Update(existingBook);
            return true;
        }

        public bool DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
                return false;

            _bookRepository.Delete(id); 
            return true;

            //lo hice de esta manera para evitar modificar RepositoryBase.
        }
    }
}