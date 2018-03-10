using AutoMapper;
using Bookstore.Domain.CommandHandlers.Commands.Book;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Repositories;
using Bookstore.Framework.CommandHandlers;
using Bookstore.Framework.Specifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bookstore.Domain.CommandHandlers
{
    public class BookCommandHandler :
        IRequestHandler<BookInsertCommand, ICommandResult>,
        IRequestHandler<BookUpdateCommand, ICommandResult>,
        IRequestHandler<BookDeleteCommand, ICommandResult>
    {
        public BookCommandHandler(IMapper mapper, IBookRepository bookRepository)
        {
            this.Mapper = mapper;
            this.BookRepository = bookRepository;
        }

        public IMapper Mapper { get; }
        public IBookRepository BookRepository { get; }

        public async Task<ICommandResult> Handle(BookInsertCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<BookInsertCommand, Book>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.BookRepository.InsertAsync(entity);

            await this.BookRepository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(BookUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<BookUpdateCommand, Book>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.BookRepository.UpdateAsync(entity);

            await this.BookRepository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(BookDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.BookRepository.GetAsync(request.Id);

            if (entity == null)
            {
                return new FailureResult { Result = "Livro não encontrado" };
            }

            await this.BookRepository.DeleteAsync(entity);

            await this.BookRepository.CommitAsync();

            return new SuccessResult();
        }

        private ICommandResult validate(Book entity)
        {
            ICommandResult result = new FailureResult();
            StringRequiredSpec stringRequired = new StringRequiredSpec(nameof(entity.Title));

            if (!stringRequired.IsSatisfiedBy(entity.Title))
            {
                result.Result = stringRequired.Description;
                return result;
            }
            MaxLenghtSpec maxLenghtSpec = new MaxLenghtSpec(nameof(entity.Title), 250);
            if (!maxLenghtSpec.IsSatisfiedBy(entity.Title))
            {
                result.Result = maxLenghtSpec.Description;
                return result;
            }

            stringRequired = new StringRequiredSpec(nameof(entity.Author));
            if (!stringRequired.IsSatisfiedBy(entity.Author))
            {
                result.Result = stringRequired.Description;
                return result;
            }
            maxLenghtSpec = new MaxLenghtSpec(nameof(entity.Author), 250);
            if (!maxLenghtSpec.IsSatisfiedBy(entity.Author))
            {
                result.Result = maxLenghtSpec.Description;
                return result;
            }

            NumberMoreThanZero numberMoreThanZero = new NumberMoreThanZero(nameof(entity.Year));
            if (!numberMoreThanZero.IsSatisfiedBy(entity.Year))
            {
                result.Result = numberMoreThanZero.Description;
                return result;
            }

            return result;
        }
    }
}
