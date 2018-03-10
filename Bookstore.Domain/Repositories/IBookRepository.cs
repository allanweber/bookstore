using Bookstore.Domain.Entities;
using Bookstore.Framework.Repositories;

namespace Bookstore.Domain.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
    }
}
