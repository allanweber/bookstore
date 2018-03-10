using System.Collections.Generic;
using System.Threading.Tasks;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Repositories;
using Bookstore.Framework.Repositories;
using System.Linq;

namespace Bookstore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<List<Book>> GetAllAsync()
        {
            return base.Query().OrderBy(b => b.Title).ToList();
        }
    }
}
