using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Web.Contracts;
using SIGEBI.Web.Models;

namespace SIGEBI.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return View(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users.");
                return View(new List<UserModel>());
            }         
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user details for ID {Id}.", id);
                return NotFound();
            }

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
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                await _userRepository.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating user.");
            }
            return View(model);
        }

        // GET: UserController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserCreateAndUpdateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(id);
                if (existingUser == null)
                    return NotFound();
                
                await _userRepository.UpdateAsync(id, model);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing user with ID {Id}.", id);
            }
            return View(model);
        }
        //revisar

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                return user == null ? NotFound() : View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user for delete with ID {Id}.", id);
                return NotFound();
            }

        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _userRepository.DeleteAsync(id);
                if (success)
                {
                    _logger.LogInformation("User with ID {Id} deleted successfully.", id);
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "The user could not be deleted.");
                return View();
            }
            catch
            {
                _logger.LogError("Error deleting user with ID {Id}.", id);
                return View();
            }
        }
    }
}
