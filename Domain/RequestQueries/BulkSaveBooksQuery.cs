using BookStoreAPI._Core.ResponseModels;
using MediatR;

namespace BookStoreAPI.Domain.RequestQueries
{
    public class BulkSaveBooksQuery : IRequest<Result<int>>
    {
        public List<BooksDetails>? booksDetails { get; set; }
    }

    public class BooksDetails
    {
        public int? Id { get; set; }
        public string? Publisher { get; set; }
        public string? Title { get; set; }
        public string? AuthorLastName { get; set; }
        public string? AuthorFirstName { get; set; }
        public decimal? Price { get; set; }
    }
}
