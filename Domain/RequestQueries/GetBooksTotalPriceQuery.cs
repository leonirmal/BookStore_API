using BookStoreAPI._Core.ResponseModels;
using MediatR;

namespace BookStoreAPI.Domain.RequestQueries
{
    public class GetBooksTotalPriceQuery : IRequest<Result<decimal>>
    {

    }
}
