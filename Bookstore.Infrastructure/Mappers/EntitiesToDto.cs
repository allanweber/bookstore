using AutoMapper;
using Bookstore.Domain.Dtos;
using Bookstore.Domain.Entities;

namespace Bookstore.Infrastructure.Mappers
{
    public class EntitiesToDto: Profile
    {
        public EntitiesToDto()
        {
            this.CreateMap<Book, BookDto>();
        }
    }
}
