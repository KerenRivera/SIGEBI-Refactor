using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Interfaces;
using SIGEBI.Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace SIGEBI.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UsersDto> GetAllUsers()
        {
            return _userRepository.GetAll()
                .Select(user => new UsersDto
                {
                    Id = user.Id,
                    Name = user.Name
                });
        }

        public UsersDto GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return null;

            return new UsersDto
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public void AddUser(UsersDto dto)
        {
            var user = new User
            {
                Name = dto.Name
            };
            _userRepository.Add(user);
        }

        public void UpdateUser(int id, string name)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return;

            user.Name = name;
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }
    }
}
