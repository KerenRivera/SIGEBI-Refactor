using System.Net.Http.Json;
using System.Text.Json;
using SIGEBI.Web.Contracts;
using SIGEBI.Web.Models;

namespace SIGEBI.Web.Repositories
{

    public class ApiBookRepository : IBookRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiBookRepository> _logger;
        private const string Endpoint = "Book";

        public ApiBookRepository(HttpClient httpClient, ILogger<ApiBookRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<BookModel>> GetAllBooksAsync() 
        {
            try
            {
                var response = await _httpClient.GetAsync(Endpoint);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<BookModel>>() ?? new List<BookModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books.");
                return new List<BookModel>(); 
            }
        }

        public async Task<BookModel> GetBookByIdAsync(int id) 
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Endpoint}/{id}");
                if (!response.IsSuccessStatusCode)
                { 
                    _logger.LogWarning("Failed to fetch book with ID {Id}. Status code: {StatusCode}", id, response.StatusCode);
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<BookModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book with ID {Id}.", id);
                return null;
            }
            
            
        }

        public async Task<bool> CreateAsync(BookModel book) 
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(Endpoint, book);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to create book. Status code: {StatusCode}", response.StatusCode);
                    return false;
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating book.");
                return false;
            }
           
        }

        public async Task<bool> DeleteAsync(int id) 
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to delete book with ID {Id}. Status code: {StatusCode}", id, response.StatusCode);
                    return false;
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book with ID {Id}.", id);
                return false;
            }
           
            
        }

        public async Task<bool> UpdateAsync(BookModel book)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{book.id}", book);
                if (!response.IsSuccessStatusCode)
                { 
                    _logger.LogWarning("Failed to update book with ID {Id}. Status code: {StatusCode}", book.id, response.StatusCode);
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating book with ID {Id}.", book.id);
                return false;
            }
        }
    }
}
