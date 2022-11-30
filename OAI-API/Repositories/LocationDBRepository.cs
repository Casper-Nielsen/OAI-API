using Dapper;
using OAI_API.Shared;

namespace OAI_API.Repositories
{
    /// <summary>
    /// Gets the route from a predefined route
    /// </summary>
    public class LocationDBRepository : DBRepository, ILocationRepository
    {
        public LocationDBRepository(IDatabaseFactory connectionFactory) : base(connectionFactory) { }

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
