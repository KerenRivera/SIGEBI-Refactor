using SIGEBI.Web.Contracts;
using SIGEBI.Web.Models;
namespace SIGEBI.Web.Repositories
{
    public class ApiUserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiUserRepository> _logger;
        private const string Endpoint = "User";
        public ApiUserRepository(HttpClient httpClient, ILogger<ApiUserRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(Endpoint);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<UserModel>>() ?? new List<UserModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users.");
                return new List<UserModel>();
            }
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            try
            { 
                var response = await _httpClient.GetAsync($"{Endpoint}/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to fetch user with ID {Id}. Status code: {StatusCode}", id, response.StatusCode);
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<UserModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user with ID {Id}.", id);
                return null;
            }
        }

        public async Task<bool> CreateAsync(UserCreateAndUpdateModel user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(Endpoint, user);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                _logger.LogWarning("Failed to create user. Status code: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user.");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, UserCreateAndUpdateModel user)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{user.id}", user);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                _logger.LogWarning("Failed to update user with ID {Id}. Status code: {StatusCode}", user.id, response.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {Id}.", user.id);
                return false;
            }

        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                _logger.LogWarning("Failed to delete user with ID {Id}. Status code: {StatusCode}", id, response.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {Id}.", id);
                return false;
            }
        }
    }
}
