using AutoMapper;
using Bookstore.Domain.CommandHandlers.Commands.Book;
using Bookstore.Domain.Entities;

namespace Bookstore.Infrastructure.Mappers
{
    public class DtoToEntities: Profile
    {
        public DtoToEntities()
        {
            this.CreateMap<BookInsertCommand, Book>();
            this.CreateMap<BookUpdateCommand, Book>();
        }
    }
}
