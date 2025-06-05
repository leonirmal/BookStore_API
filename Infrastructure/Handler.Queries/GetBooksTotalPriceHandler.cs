using BookStoreAPI._Core.ResponseModels;
using BookStoreAPI.Domain.RequestQueries;
using BookStoreAPI.Domain.ResponseModels.cs;
using BookStoreAPI.Infrastructure.Persistence.Context;
using MediatR;
using System.Net;

namespace BookStoreAPI.Infrastructure.Handler.Queries
{
    public class GetBooksTotalPriceHandler : IRequestHandler<GetBooksTotalPriceQuery, Result<decimal>>
    {
        private readonly ILogger<GetBooksTotalPriceHandler> _logger;
        readonly ApplicationReadContext _readContext;
        public GetBooksTotalPriceHandler(ILogger<GetBooksTotalPriceHandler> logger, ApplicationReadContext readContext)
        {
            _logger = logger;
            _readContext = readContext;
        }
        public async Task<Result<decimal>> Handle(GetBooksTotalPriceQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Books TotalPrice Handler Method Hit");
            Result<decimal> result = new Result<decimal>();
            try
            {
                result.Data = (decimal)_readContext.Books.Select(s => s.Price).Sum(); 
                result.IsSuccess = true;
                result.Message = "Success";
                result.StatusCode = (int)HttpStatusCode.OK;
                _logger.LogInformation("Books TotalPrice calculated successfully and returning response");
                return result;                        
            }
            catch (Exception ex)
            {
                _logger.LogError("Get Books TotalPrice Handler exception occured. Exception : " + ex.Message);
                return result;
            }
        }
    }
}
