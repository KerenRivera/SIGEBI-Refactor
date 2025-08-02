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

        public BookController(IBookRepository bookRepository) //implementacion de DI
        {
            _bookRepository = bookRepository;
        }
        // GET: BookController
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return View(books);

        }

        // GET: BookController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookModel book)
        {
            var success = await _bookRepository.CreateAsync(book);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // GET: BookController/Edit/5
        // SRP, RP, DIP
        public async Task<IActionResult> Edit(int id)
        {
           var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        // aaaaaaaah. que bonito se ve ahora
        public async Task<IActionResult> Edit(BookModel model)
        {
            var book = await _bookRepository.GetBookByIdAsync(model.id);
            if (book == null)
            {
                return NotFound();
            }

            var success = await _bookRepository.UpdateAsync(model);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _bookRepository.DeleteAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();

        }

        // de esta forma el controller solo maneja http (SRP)
    }
}
