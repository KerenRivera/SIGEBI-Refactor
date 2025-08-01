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

                    client.BaseAddress = new Uri("https://localhost:7166/api/");

                    var response = await client.GetAsync("Book");
                    Console.WriteLine(response.StatusCode);

                    if (response.IsSuccessStatusCode) // devuelve un http 200 OK 
                    { 
                        var responseString = await response.Content.ReadAsStringAsync();

                        books = JsonSerializer.Deserialize<List<BookModel>>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
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
        public async Task<IActionResult> Details(int id)
        {
            BookModel book = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7166/api/");

                    var response = await client.GetAsync($"Book/{id}"); //agregar validacion de id
                    Console.WriteLine(response.StatusCode);

                    if (response.IsSuccessStatusCode) // devuelve un http 200 OK 
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        book = JsonSerializer.Deserialize<BookModel>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }

            if (book == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> Edit(int id)
        {
            BookModel book = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7166/api/");

                    var response = await client.GetAsync($"Book/{id}"); //agregar validacion de id
                    Console.WriteLine(response.StatusCode);

                    if (response.IsSuccessStatusCode) // devuelve un http 200 OK 
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        book = JsonSerializer.Deserialize<BookModel>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
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
