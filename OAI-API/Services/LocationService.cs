using OAI_API.Repositories;

namespace OAI_API.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Task<string> GetDirectionAsync(string location)
        {
            return _locationRepository.GetDirectionsToLocationAsync(location);
        }
    }
}
