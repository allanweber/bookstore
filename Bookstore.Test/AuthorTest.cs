using Bookstore.Api;
using Bookstore.Domain.CommandHandlers.Commands.Book;
using Bookstore.Domain.Dtos;
using Bookstore.Framework.CommandHandlers;
using Bookstore.Framework.Test;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

[assembly: CollectionBehavior(
    CollectionBehavior.CollectionPerAssembly,
    DisableTestParallelization = true,
    MaxParallelThreads = 1)]

namespace Bookstore.Test
{   
    public class AuthorTest : IClassFixture<WebHostFixture<Startup>>
    {
        public AuthorTest(WebHostFixture<Startup> webHostFixture)
        {
            WebHostFixture = webHostFixture;
        }

        public WebHostFixture<Startup> WebHostFixture { get; }

        private string path = "api/v1/book";

        [Fact]
        public async Task InsertAsync()
        {
            var insert = new BookInsertCommand { Author = "", Title = "", Year = 1984 };
            var res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.BadRequest, "Deveria ser status 400.");
            var command = await res.Content.ReadAsObjectAsync<FailureResult>();
            Assert.True(command.IsFailure, "Command deveria ter falhado");

            insert = new BookInsertCommand { Author = "", Title = "Game of Thrones", Year = 1984 };
            res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.BadRequest, "Deveria ser status 400.");
            command = await res.Content.ReadAsObjectAsync<FailureResult>();
            Assert.True(command.IsFailure, "Command deveria ter falhado");

            insert = new BookInsertCommand { Author = "R R Matin", Title = "Game of Thrones", Year = 0 };
            res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.BadRequest, "Deveria ser status 400.");
            command = await res.Content.ReadAsObjectAsync<FailureResult>();
            Assert.True(command.IsFailure, "Command deveria ter falhado");

            insert = new BookInsertCommand { Author = "R R Matin", Title = "Game of Thrones", Year = 1985 };
            res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            command = await res.Content.ReadAsObjectAsync<FailureResult>();
            Assert.True(command.IsSuccess, "Command deveria ter tido sucesso");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var insert = new BookInsertCommand { Author = "R R M", Title = "Game of Thrones", Year = 1985 };
            var res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            var command = await res.Content.ReadAsObjectAsync<SuccessResult>();
            Assert.True(command.IsSuccess, "Command deveria ter tido sucesso");

            res = await this.WebHostFixture.TestClient.GetAsync(this.path);
            var dto = await res.Content.ReadAsObjectAsync<List<BookDto>>();
            Assert.True(dto.Count == 1, "Dto deveria ter 1 registro");
            Assert.True(dto[0].Author == "R R M", "Autor deveria ser R R M");

            var update = new BookUpdateCommand { Id = 1, Author = "", Title = "Game of Thrones", Year = 1985 };
            res = await this.WebHostFixture.TestClient.PutAsObjectAsync(this.path, update);
            Assert.True(res.StatusCode == HttpStatusCode.BadRequest, "Deveria ser status 400.");
            var commandfail = await res.Content.ReadAsObjectAsync<FailureResult>();
            Assert.True(commandfail.IsFailure, "Command deveria ter falhado");

            update = new BookUpdateCommand { Id = 1, Author = "RR Martin", Title = "", Year = 1985 };
            res = await this.WebHostFixture.TestClient.PutAsObjectAsync(this.path, update);
            Assert.True(res.StatusCode == HttpStatusCode.BadRequest, "Deveria ser status 400.");
            commandfail = await res.Content.ReadAsObjectAsync<FailureResult>();
            Assert.True(commandfail.IsFailure, "Command deveria ter falhado");

            update = new BookUpdateCommand { Id = 1, Author = "RR Martin", Title = "Game of Thrones", Year = 0 };
            res = await this.WebHostFixture.TestClient.PutAsObjectAsync(this.path, update);
            Assert.True(res.StatusCode == HttpStatusCode.BadRequest, "Deveria ser status 400.");
            commandfail = await res.Content.ReadAsObjectAsync<FailureResult>();
            Assert.True(commandfail.IsFailure, "Command deveria ter falhado");

            update = new BookUpdateCommand { Id = 0, Author = "RR Martin", Title = "Game of Thrones", Year = 1985 };
            res = await this.WebHostFixture.TestClient.PutAsObjectAsync(this.path, update);
            Assert.True(res.StatusCode == HttpStatusCode.BadRequest, "Deveria ser status 400.");
            commandfail = await res.Content.ReadAsObjectAsync<FailureResult>();
            Assert.True(commandfail.IsFailure, "Command deveria ter falhado");

            update = new BookUpdateCommand { Id = 1, Author = "RR Martin", Title = "Game of Thrones", Year = 1985 };
            res = await this.WebHostFixture.TestClient.PutAsObjectAsync(this.path, update);
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            command = await res.Content.ReadAsObjectAsync<SuccessResult>();
            Assert.True(command.IsSuccess, "Command deveria ter tido sucesso");

            res = await this.WebHostFixture.TestClient.GetAsync(this.path);
            dto = await res.Content.ReadAsObjectAsync<List<BookDto>>();
            Assert.True(dto.Count == 1, "Dto deveria ter 1 registro");
            Assert.True(dto[0].Author == "RR Martin", "Autor deveria ser RR Martin");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var insert = new BookInsertCommand { Author = "R R M", Title = "Game of Thrones", Year = 1985 };
            var res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            var commandSuccess = await res.Content.ReadAsObjectAsync<SuccessResult>();
            Assert.True(commandSuccess.IsSuccess, "Command deveria ter tido sucesso");

            insert = new BookInsertCommand { Author = "Outro Autor", Title = "Outro Livro", Year = 1900 };
            res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            commandSuccess = await res.Content.ReadAsObjectAsync<SuccessResult>();
            Assert.True(commandSuccess.IsSuccess, "Command deveria ter tido sucesso");

            res = await this.WebHostFixture.TestClient.GetAsync(this.path);
            var dto = await res.Content.ReadAsObjectAsync<List<BookDto>>();
            Assert.True(dto.Count == 2, "Dto deveria ter 2 registros");

            res = await this.WebHostFixture.TestClient.DeleteAsync($"{this.path}/0");
            Assert.True(res.StatusCode == HttpStatusCode.BadRequest, "Deveria ser status 400.");
            var commandFail = await res.Content.ReadAsObjectAsync<FailureResult>();
            Assert.True(commandFail.IsFailure, "Command deveria ter falhado");

            res = await this.WebHostFixture.TestClient.DeleteAsync($"{this.path}/1");
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            commandSuccess = await res.Content.ReadAsObjectAsync<SuccessResult>();
            Assert.True(commandSuccess.IsSuccess, "Command deveria ter tido sucesso");

            res = await this.WebHostFixture.TestClient.GetAsync(this.path);
            dto = await res.Content.ReadAsObjectAsync<List<BookDto>>();
            Assert.True(dto.Count == 1, "Dto deveria ter 1 registro");

        }

        [Fact]
        public async Task GetAsync()
        {
            var insert = new BookInsertCommand { Author = "R R M", Title = "Game of Thrones", Year = 1985 };
            var res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            var commandSuccess = await res.Content.ReadAsObjectAsync<SuccessResult>();
            Assert.True(commandSuccess.IsSuccess, "Command deveria ter tido sucesso");

            insert = new BookInsertCommand { Author = "Outro Autor", Title = "Outro Livro", Year = 1900 };
            res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            commandSuccess = await res.Content.ReadAsObjectAsync<SuccessResult>();
            Assert.True(commandSuccess.IsSuccess, "Command deveria ter tido sucesso");

            res = await this.WebHostFixture.TestClient.GetAsync($"{this.path}/1");
            var dto = await res.Content.ReadAsObjectAsync<BookDto>();
            Assert.True(dto.Author == "R R M", "Autor deveria ser R R M");

            res = await this.WebHostFixture.TestClient.GetAsync($"{this.path}/2");
            dto = await res.Content.ReadAsObjectAsync<BookDto>();
            Assert.True(dto.Author == "Outro Autor", "Autor deveria ser Outro Autor");

            res = await this.WebHostFixture.TestClient.GetAsync($"{this.path}/3");
            Assert.True(res.StatusCode == HttpStatusCode.NotFound, "Deveria ser status 404.");
        }

        [Fact]
        public async Task GetAllAsync()
        {
            var insert = new BookInsertCommand { Author = "R R M", Title = "Game of Thrones", Year = 1985 };
            var res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            var commandSuccess = await res.Content.ReadAsObjectAsync<SuccessResult>();
            Assert.True(commandSuccess.IsSuccess, "Command deveria ter tido sucesso");

            insert = new BookInsertCommand { Author = "Outro Autor", Title = "Biblia", Year = 1900 };
            res = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, insert);
            Assert.True(res.StatusCode == HttpStatusCode.OK, "Deveria ser status 200.");
            commandSuccess = await res.Content.ReadAsObjectAsync<SuccessResult>();
            Assert.True(commandSuccess.IsSuccess, "Command deveria ter tido sucesso");

            res = await this.WebHostFixture.TestClient.GetAsync(this.path);
            var dto = await res.Content.ReadAsObjectAsync<List<BookDto>>();
            Assert.True(dto[0].Title== "Biblia", "Primeiro Livro de veria ser biblia");
        }
    }
}
