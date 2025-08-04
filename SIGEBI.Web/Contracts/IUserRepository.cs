using SIGEBI.Web.Models;

namespace SIGEBI.Web.Contracts
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<bool> CreateAsync(UserCreateAndUpdateModel user);
        Task<bool> UpdateAsync(int id, UserCreateAndUpdateModel user);
        Task<bool> DeleteAsync(int id);
    }
}
