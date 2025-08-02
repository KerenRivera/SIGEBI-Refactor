using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Interfaces;


namespace SIGEBI.Application.Services
{
    public class BookService
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
        // modificando para que verifique que el libro existe antes de actualizar
        //public void UpdateBook(Book book)
        //{
        //    _bookRepository.Update(book);
        //}
        public bool UpdateBook(Book book)
        {
            var existingBook = _bookRepository.GetById(book.Id);
            if (existingBook == null)
                return false; // El libro no existe, no se puede actualizar

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;

            _bookRepository.Update(existingBook);
            return true;
        }

        public void DeleteBook(int id)
        {
            _bookRepository.Delete(id);
        }
    }
}