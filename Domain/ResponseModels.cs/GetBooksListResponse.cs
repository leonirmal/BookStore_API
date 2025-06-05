using BookStoreAPI.Domain.SPModels;
using MediatR;

namespace BookStoreAPI.Domain.ResponseModels.cs
{
    public class GetBooksListResponse
    {   
        public int Id { get; set; }
        public string? Publisher { get; set; }
        public string? Title { get; set; }
        public string? AuthorLastName { get; set; }
        public string? AuthorFirstName { get; set; }
        public decimal? Price { get; set; }

        public static implicit operator GetBooksListResponse(SPGetBooksList res)
        {
            return new GetBooksListResponse { 
                Id = res.id, 
                Publisher = res.publisher, 
                Title = res.title,
                AuthorFirstName = res.authorFirstName,
                AuthorLastName = res.authorLastName,
                Price = res.price
            };
        }
    }    
}
