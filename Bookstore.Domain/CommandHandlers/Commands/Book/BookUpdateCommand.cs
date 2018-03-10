namespace Bookstore.Domain.CommandHandlers.Commands.Book
{
    public class BookUpdateCommand: BookInsertCommand
    {
        public int Id { get; set; }
    }
}
