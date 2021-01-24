using MediatR;
using StockTicker.Domain.Response;
using StockTicker.Service.Data.Models;
using System.Threading.Tasks;

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
