using Dapper;
using OAI_API.Shared;

namespace OAI_API.Repositories
{
    public class LocationDBRepository : ILocationRepository
    {
        private readonly IDatabaseFactory _connectionFactory;

        public LocationDBRepository(IDatabaseFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<string> GetDirectionsToLocationAsync(string location)
        {
            var connection = await _connectionFactory.GetConnection();

            var directions = await connection.QueryFirstOrDefaultAsync<string>($@"
                SELECT 
                    Directions 
                FROM Location 
                WHERE LocationName = @LocationName", new
            {
                LocationName = location
            });

            return directions;
        }
    }
}
