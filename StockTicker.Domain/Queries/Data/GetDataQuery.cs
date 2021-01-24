using System.Collections.Generic;
using StockTicker.Service.Data.Models;
using MediatR;

namespace StockTicker.Domain.Queries.Data
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<Company>>
    {
        public GetAllCompaniesQuery()
        {
        }
    }

    public class GetCompanyByIdQuery : IRequest<Company>
    {
        public int _id { get; private set; }

        public GetCompanyByIdQuery(int id)
        {
            _id = id;
        }
    }

    public class GetCompanyByIsinQuery : IRequest<Company>
    {
        public string _isin { get; private set; }

        public GetCompanyByIsinQuery(string isin)
        {
            _isin = isin;
        }
    }
}
