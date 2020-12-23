using MediatR;
using StockTicker.Service.Data.Models;

namespace StockTicker.Domain.Commands.Data
{
    public class CreateCompanyCommand : IRequest<Company>
    {
        public Company _company { get; private set; }

        public CreateCompanyCommand(Company company)
        {
            _company = company;
        }
    }
}
