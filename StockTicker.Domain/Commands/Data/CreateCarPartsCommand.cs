using MediatR;
using CarParts.Service.Data.Models;

namespace CarParts.Domain.Commands.Data
{
    public class CreateCarPartsCommand : IRequest<CarPart>
    {
        public CarPart _carPart { get; private set; }

        public CreateCarPartsCommand(CarPart carPart)
        {
            _carPart = carPart;
        }
    }
}