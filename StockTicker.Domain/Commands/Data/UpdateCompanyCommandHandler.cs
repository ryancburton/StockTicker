using MediatR;
using System.Threading;
using StockTicker.Service.Data.Models;
using StockTicker.Service.Data.Services;
using System.Threading.Tasks;

namespace StockTicker.Domain.Commands.Data
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Company>
    {
        private readonly ICompanyDBService _companyDBService;

        public UpdateCompanyCommandHandler(ICompanyDBService companyDBService)
        {
            _companyDBService = companyDBService;
        }

        public async Task<Company> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            await _companyDBService.UpdateExsistingCompanyAsync(request._company);
            return request._company;
        }
    }
}
