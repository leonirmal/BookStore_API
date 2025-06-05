using BookStoreAPI._Core.ResponseModels;
using BookStoreAPI.Domain.RequestQueries;
using BookStoreAPI.Domain.ResponseModels.cs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        protected virtual IMediator _mediator { get; set; }
        public StoreController(ILogger<StoreController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("get-books-list")]
        public async Task<Result<List<GetBooksListResponse>>> GetBooksList(GetBooksListQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetBooksList Method has been Hit");
            return await _mediator.Send(query);
        }

        [HttpPost("booksList-authorTitle-based")]
        public async Task<Result<List<GetBooksListResponse>>> GetBooksList(GetBooksListAuthorTitleQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetBooksListAuthorTitle Method has been Hit");
            return await _mediator.Send(query);
        }

        [HttpPost("get-books-totalPrice")]
        public async Task<Result<decimal>> GetBooksTotalPrice(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetBooksTotalPrice Method has been Hit");
            GetBooksTotalPriceQuery query = new GetBooksTotalPriceQuery();
            return await _mediator.Send(query);
        }

        [HttpPost("bulk-save-books")]
        public async Task<Result<int>> BulkSaveBooks(BulkSaveBooksQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetBooksTotalPrice Method has been Hit");
            return await _mediator.Send(query);
        }
    }
}
