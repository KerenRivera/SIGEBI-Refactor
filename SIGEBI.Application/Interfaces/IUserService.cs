using SIGEBI.Application.DTOs;

namespace SIGEBI.Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UsersDto> GetAllUsers();
        UsersDto GetUserById(int id);
        void AddUser(UsersDto dto);
        void UpdateUser(int id, string name);
        void DeleteUser(int id);
    }
}
