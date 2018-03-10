using Bookstore.Framework.Entities;

namespace Bookstore.Domain.Entities
{
    public class Book: BaseEntity
    {
        public string Title { get; private set; }

        public string Author { get; private set; }

        public int Year { get; private set; }
    }
}
