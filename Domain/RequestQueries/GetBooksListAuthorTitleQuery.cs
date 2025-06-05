using BookStoreAPI._Core.ResponseModels;
using BookStoreAPI.Domain.ResponseModels.cs;
using MediatR;

namespace BookStoreAPI.Domain.RequestQueries
{
    public class GetBooksListAuthorTitleQuery : IRequest<Result<List<GetBooksListResponse>>>
    {
        public string? Title { get; set; }
        public string? AuthorLastName { get; set; }
        public string? AuthorFirstName { get; set; }
    }
}
