using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using StockTicker.Service.DAL.Models;
using StockTicker.Service.DAL.Service;

namespace StockTicker.Domain.Queries.Data
{
    public class GetDataAllQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<Company>>
    {
        private readonly ICompanyDBService _companyDBService;

        public GetDataAllQueryHandler(ICompanyDBService companyDBService)
        {
            _companyDBService = companyDBService;
        }

        public async Task<IEnumerable<Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _companyDBService.GetAllCompaniesAsync();
        }
    }

    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Company>
    {
        private readonly ICompanyDBService _companyDBService;

        public GetCompanyByIdQueryHandler(ICompanyDBService companyDBService)
        {
            _companyDBService = companyDBService;
        }

        public async Task<Company> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            if (request._id.Equals(null))
            {
                throw new ArgumentException("Company ID cannot be null");
            }

            return await _companyDBService.FindCompanyByIdAsync(request._id);
        }
    }

    public class GetCompanyByIsinQueryHandler : IRequestHandler<GetCompanyByIsinQuery, Company>
    {
        private readonly ICompanyDBService _companyDBService;

        public GetCompanyByIsinQueryHandler(ICompanyDBService companyDBService)
        {
            _companyDBService = companyDBService;
        }

        public async Task<Company> Handle(GetCompanyByIsinQuery request, CancellationToken cancellationToken)
        {
            if (request._isin.Equals(null))
            {
                throw new ArgumentException("Isin cannot be null");
            }

            return await _companyDBService.FindCompanyByIsinAsync(request._isin);
        }
    }
}
