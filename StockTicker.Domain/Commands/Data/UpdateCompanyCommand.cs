using MediatR;
using StockTicker.Domain.Response;
using StockTicker.Service.DATA.Models;

namespace StockTicker.Domain.Commands.Data
{
    public class UpdateCompanyCommand : IRequest<Company>
    {
        public Company _company { get; private set; }

        public UpdateCompanyCommand(Company company)
        {
            _company = company;
        }
    }
}
