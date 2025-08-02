using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Web.Models;
using SIGEBI.Web.Repositories;

namespace SIGEBI.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository) //implementacion de DI en vez de una clase
        {
            _bookRepository = bookRepository;
        }

        //metodo generico para manejar errores de llamadas a la API
        private async Task<T?> ExecuteApiCall<T>(Func<Task<T>> apiCall) //eliminamos try-catch
        {
            try
            {
                return await apiCall();
            }
            catch (Exception ex)
            {              
                Console.WriteLine($"Error:  + ex.Message");
                return default;
            }
        }

        // GET: BookController
        public async Task<IActionResult> Index()
        {
            var books = await ExecuteApiCall(() => _bookRepository.GetAllBooksAsync());
            return View(books ?? new List<BookModel>());

        }

        // GET: BookController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await ExecuteApiCall(() => _bookRepository.GetBookByIdAsync(id));
            return book == null ? NotFound() : View(book);
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
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            var success = await ExecuteApiCall(() => _bookRepository.CreateAsync(book));
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "The book could not be created");
            return View(book);
        }

        // GET: BookController/Edit/5
        // SRP, RP, DIP
        public async Task<IActionResult> Edit(int id)
        {
           var book = await ExecuteApiCall(() => _bookRepository.GetBookByIdAsync(id));
            return book == null ? NotFound() : View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, BookModel book)
        {
            if (id != book.id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(book);

            var success = await ExecuteApiCall(() => _bookRepository.UpdateAsync(book));

            if (success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "No se pudo actualizar el libro.");
            return View(book);

        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var book = await ExecuteApiCall(() => _bookRepository.GetBookByIdAsync(id));
            return book == null ? NotFound() : View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await ExecuteApiCall(() => _bookRepository.DeleteAsync(id));
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "The book could not be deleted.");
            return RedirectToAction(nameof(Delete), new { id });

        }
    }
}
