using System.Net.Http.Json;
using System.Text.Json;
using SIGEBI.Web.Models;

namespace SIGEBI.Web.Repositories
{
    /*
    aqui implementamos el patrón de repositorio para acceder a los datos de los libros de manera centralizada.
    estamos reutilizando codigo, asi evitamos repetir esto en los controladores.
    */
    public class ApiBookRepository : IBookRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7166/api/Book";

        public ApiBookRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<BookModel>>() ?? new List<BookModel>();
        }

        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<BookModel>();
        }

        public async Task<bool> CreateAsync(BookModel book)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, book);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(BookModel book)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{book.id}", book);
            return response.IsSuccessStatusCode;
        }
    }
}
