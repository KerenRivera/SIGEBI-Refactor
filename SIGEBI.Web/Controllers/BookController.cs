using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Web.Contracts;
using SIGEBI.Web.Models;

namespace SIGEBI.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository bookRepository, ILogger<BookController> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        // GET: BookController
        public async Task<IActionResult> Index()
        {
            try
            {
                var books = await _bookRepository.GetAllBooksAsync();
                return View(books ?? new List<BookModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books in Index action.");
                return View(new List<BookModel>()); 
            }
        }

        // GET: BookController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id);
                return book == null ? NotFound() : View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book details for ID {Id}.", id);
                return NotFound(); 
            }

        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        //SRP, Repository, DI, DIP.
        public async Task<IActionResult> Create(BookModel book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(book);
                }

                var success = await _bookRepository.CreateAsync(book);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating book.");
            }
            return View(book);
        }

        // GET: BookController/Edit/5
        // SRP, RP, DIP
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id);
                return book == null ? NotFound() : View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book for edit with ID {Id}.", id);
                return NotFound();
            }

        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, BookModel book)
        {
            try
            {
                if (id != book.id)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(book);

                var success = await _bookRepository.UpdateAsync(book);

                if (success)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating book with ID {Id}.", id);
                ModelState.AddModelError("", "The book could not be updated.");
            }
            return View(book);

        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id);
                return book == null ? NotFound() : View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book for deletion with ID {Id}.", id);
                return NotFound();
            }
            
        }

        // POST: BookController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _bookRepository.DeleteAsync(id);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book with ID {Id}.", id);
                ModelState.AddModelError("", "The book could not be deleted.");
            }
            return RedirectToAction(nameof(Delete), new { id });

        }
    }
}
