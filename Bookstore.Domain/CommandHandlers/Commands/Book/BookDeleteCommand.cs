using Bookstore.Framework.CommandHandlers;
using Bookstore.Framework.Entities;
using MediatR;

namespace Bookstore.Domain.CommandHandlers.Commands.Book
{
    public class BookDeleteCommand: BaseEntity, IRequest<ICommandResult>
    {
    }
}
