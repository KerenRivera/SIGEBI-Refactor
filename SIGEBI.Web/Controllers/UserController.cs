using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Web.Models;

namespace SIGEBI.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        public async Task<IActionResult> Index()
        {
            List<UserModel> users = new();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7166/api/");

                    var response = await client.GetAsync("User");
                    Console.WriteLine(response.StatusCode);

                    if (response.IsSuccessStatusCode) // devuelve un http 200 OK 
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        users = JsonSerializer.Deserialize<List<UserModel>>(responseString, new JsonSerializerOptions
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
            return View(users);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            UserModel user = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7166/api/");

                    var response = await client.GetAsync($"User/{id}");
                    Console.WriteLine(response.StatusCode);

                    if (response.IsSuccessStatusCode) // devuelve un http 200 OK 
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        user = JsonSerializer.Deserialize<UserModel>(responseString, new JsonSerializerOptions
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

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateAndUpdateModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7166/api/");

                    var response = await client.PostAsJsonAsync("User", model);

                    if (response.IsSuccessStatusCode) // devuelve un http 200 OK 
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);              
            }
            return View(model);
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
