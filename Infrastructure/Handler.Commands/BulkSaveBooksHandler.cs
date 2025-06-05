using BookStoreAPI._Core.ResponseModels;
using BookStoreAPI._Core.ServiceFactories;
using BookStoreAPI.Domain.RequestQueries;
using BookStoreAPI.Infrastructure.Persistence.Context;
using Dapper;
using MediatR;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Net.Mail;

namespace BookStoreAPI.Infrastructure.Handler.Commands
{
    public class BulkSaveBooksHandler : IRequestHandler<BulkSaveBooksQuery, Result<int>>
    {
        private readonly ILogger<BulkSaveBooksQuery> _logger;
        private IDatabaseDapper _dapper;
        public BulkSaveBooksHandler(ILogger<BulkSaveBooksQuery> logger, IDatabaseDapper dapper)
        {
            _logger = logger;
            _dapper = dapper;
        }
        public async Task<Result<int>> Handle(BulkSaveBooksQuery request, CancellationToken cancellationToken)
        {
            Result<int> result = new Result<int>();
            DataTable tblBookStore = new DataTable();
            tblBookStore.Columns.Add("Id", typeof(int));
            tblBookStore.Columns.Add("Publisher", typeof(string));
            tblBookStore.Columns.Add("Title", typeof(string));
            tblBookStore.Columns.Add("AuthorLastName", typeof(string));
            tblBookStore.Columns.Add("AuthorFirstName", typeof(string));
            tblBookStore.Columns.Add("Price", typeof(decimal));

            DataRow TblBookStoreRows;

            try
            {
                DynamicParameters Parameters = new DynamicParameters();
                if (request.booksDetails.Count > 0)
                {
                    string strTrailer = string.Empty;
                    foreach (BooksDetails bkData in request.booksDetails.Where(X => true))
                    {
                        TblBookStoreRows = tblBookStore.NewRow();
                        TblBookStoreRows["Id"] = bkData.Id;
                        TblBookStoreRows["Publisher"] = bkData.Publisher;
                        TblBookStoreRows["Title"] = bkData.Title;
                        TblBookStoreRows["AuthorLastName"] = bkData.AuthorLastName;
                        TblBookStoreRows["AuthorFirstName"] = bkData.AuthorFirstName;
                        TblBookStoreRows["Price"] = bkData.Price;
                        tblBookStore.Rows.Add(TblBookStoreRows);
                    }
                    Parameters.Add("@bookStoreData", tblBookStore.AsTableValuedParameter("[BKS].tblBookStore"));
                }

                var set = await _dapper.Get<int>("[BKS].[SP_BULKSAVEBOOKS]", Parameters, CommandType.StoredProcedure);

                result.Data = set > 0 ? set : 0;
                result.IsSuccess = set > 0 ? true : false;
                result.Message = set > 0 ? "Success" : "Failed";
                result.StatusCode = set > 0 ? (int)HttpStatusCode.OK : (int)HttpStatusCode.Forbidden;
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
