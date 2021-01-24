using MediatR;
using CarParts.Service.Data.Models;
using System.Threading;
using StockTicker.Service.Data.Services;
using System.Threading.Tasks;

namespace CarParts.Domain.Commands.Data
{
    public class CreateCarPartsCommandHandler : IRequestHandler<CreateCarPartsCommand, CarPart>
    {
        private readonly ICompanyDBService _companyDBService;

        public CreateCarPartsCommandHandler(ICompanyDBService companyDBService)
        {
            _companyDBService = companyDBService;
        }

        public async Task<CarPart> Handle(CreateCarPartsCommand request, CancellationToken cancellationToken)
        {
            //await _companyDBService.AddNewCompanyAsync(request._carPart);
            return request._carPart;
        }
    }
}
