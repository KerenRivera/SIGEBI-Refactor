using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Web.Models;

namespace SIGEBI.Web.Controllers
{
    public class BookController : Controller
    {
        // GET: BookController
        public async Task<IActionResult> Index()
        {

            List<BookModel> books = new();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://localhost:7166/api");

                    var response = await client.GetAsync("/Book");
                    Console.WriteLine(response.StatusCode);

                    if (response.IsSuccessStatusCode) // devuelve un http 200 OK 
                    { 
                        var responseString = await response.Content.ReadAsStringAsync();
                        books = JsonSerializer.Deserialize<List<BookModel>>(responseString);
                    }
                }               
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);

            }
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
