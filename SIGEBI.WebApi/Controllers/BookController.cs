using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Interfaces; //Cambiamos Services por Interfaces
using SIGEBI.Domain.Entities;
using System.Collections.Generic;

namespace SIGEBI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService; //Dependencia abstracta para el servicio de libros

        public BookController(IBookService bookService) 
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }


        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> AddBook([FromBody] Book book)
        {
            _bookService.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest("The book id do not match the body.");

            var updated = _bookService.UpdateBook(book);
            if (!updated)
                return NotFound("The book does not exist.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}
