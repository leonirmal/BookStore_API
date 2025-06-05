using BookStoreAPI._Core.ResponseModels;
using BookStoreAPI._Core.ServiceFactories;
using BookStoreAPI.Domain.RequestQueries;
using BookStoreAPI.Domain.ResponseModels.cs;
using BookStoreAPI.Domain.SPModels;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace BookStoreAPI.Infrastructure.Handler.Queries
{
    public class GetBooksListHandler : IRequestHandler<GetBooksListQuery, Result<List<GetBooksListResponse>>>
    {
        private readonly ILogger<GetBooksListHandler> _logger;
        private IDatabaseDapper _dapper;

        public GetBooksListHandler(ILogger<GetBooksListHandler> logger, IDatabaseDapper dapper) 
        {
            _logger = logger;
            _dapper = dapper;
        }

        public async Task<Result<List<GetBooksListResponse>>> Handle(GetBooksListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Books List Handler Method Hit");
            Result<List<GetBooksListResponse>> result = new Result<List<GetBooksListResponse>>();
            List<GetBooksListResponse> response = new List<GetBooksListResponse>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@publisher", request.Publisher);
                parameters.Add("@title", request.Title);
                parameters.Add("@authorFirstName", request.AuthorFirstName);
                parameters.Add("@authorLastName", request.AuthorLastName);

                _logger.LogInformation("Get Books List SP going to hit");
                var _set = await _dapper.ExecuteGridReaderAsync("[BKS].[SP_GETBOOKSLIST]", parameters, CommandType.StoredProcedure);
                var set = (await _set.ReadAsync<SPGetBooksList>()).ToList();

                if (set.Count > 0)
                {
                    result.Data = new List<GetBooksListResponse>(set.Select(x => (GetBooksListResponse)x));
                    result.IsSuccess = true;
                    result.Message = "Success";
                    result.StatusCode = (int)HttpStatusCode.OK;
                    _logger.LogInformation("Get Books List SP response received success and response returned");
                    return result;
                }
                else
                {
                    _logger.LogInformation("Get Books List SP response received success but count was zero and response returned");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get Books List Handler exception occured. Exception : " + ex.Message);
                return result;
            }
        }
    }
}
