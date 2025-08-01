using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Interfaces;
using SIGEBI.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SIGEBI.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SIGEBIContext _context;

        public UserRepository(SIGEBIContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Set<User>().ToList();
        }

        public User GetById(int id)
        {
            return _context.Set<User>().Find(id);
        }

        public void Add(User user)
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Set<User>().Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Set<User>().Find(id);
            if (user != null)
            {
                _context.Set<User>().Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
