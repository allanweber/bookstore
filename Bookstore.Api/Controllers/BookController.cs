using AutoMapper;
using Bookstore.Domain.CommandHandlers.Commands.Book;
using Bookstore.Domain.Dtos;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Repositories;
using Bookstore.Framework.Constants;
using Bookstore.Framework.Controllers;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookstore.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class BookController :
        BaseCrudController<
            IBookRepository,
            Book,
            BookInsertCommand,
            BookUpdateCommand,
            BookDeleteCommand,
            BookDto>
    {
        public BookController(IMapper mapper, IMediator mediator, IBookRepository bookRepository)
            :base(mapper, mediator, bookRepository)
        {
        }
    }
}