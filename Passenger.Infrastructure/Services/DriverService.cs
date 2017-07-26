using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class DriverSerice : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        public DriverSerice(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }
        public async Task<DriverDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);
            return _mapper.Map<Driver,DriverDto>(driver);
        }
        public async Task RegisterAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if(driver != null)
            {
                throw new Exception($"Driver with already exists.");
            }
            var salt = Guid.NewGuid().ToString("N");
            driver = new Driver(userId);
            await _driverRepository.AddAsync(driver);
        }
    }
}