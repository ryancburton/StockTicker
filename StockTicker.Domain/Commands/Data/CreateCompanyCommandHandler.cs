using MediatR;
using StockTicker.Service.DAL.Models;
using System.Threading;
using StockTicker.Service.DAL.Services;
using System.Threading.Tasks;

namespace StockTicker.Domain.Commands.Data
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Company>
    {
        private readonly ICompanyDBService _companyDBService;

        public CreateCompanyCommandHandler(ICompanyDBService companyDBService)
        {
            _companyDBService = companyDBService;
        }

        public async Task<Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            await _companyDBService.AddNewCompanyAsync(request._company);
            return request._company;
        }
    }
}
