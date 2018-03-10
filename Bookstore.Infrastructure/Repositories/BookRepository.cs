using Bookstore.Domain.Entities;
using Bookstore.Domain.Repositories;
using Bookstore.Framework.Repositories;

namespace Bookstore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
