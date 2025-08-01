using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Interfaces;
using SIGEBI.Infrastructure.Persistence.Context;

namespace SIGEBI.Infrastructure.Persistence.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(SIGEBIContext context) : base(context)
        {
        }
    }
}