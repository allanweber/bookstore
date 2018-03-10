using Bookstore.Framework.CommandHandlers;
using MediatR;

namespace Bookstore.Domain.CommandHandlers.Commands.Book
{
    public class BookInsertCommand: IRequest<ICommandResult>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int Year { get; set; }
    }
}
