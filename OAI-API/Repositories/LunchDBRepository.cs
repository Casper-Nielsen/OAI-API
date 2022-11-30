using OAI_API.Shared;
using Dapper;
using OAI_API.Models;

namespace OAI_API.Repositories
{
    /// <summary>
    /// Gets the lunch menu from a mock set of data in the database
    /// </summary>
    public class LunchDBRepository : DBRepository, ILunchRepository
    {
        public LunchDBRepository(IDatabaseFactory connectionFactory) : base(connectionFactory) { }

        public async Task<string> GetLunchMenuAsync()
        {
            var connection = await _connectionFactory.GetConnection();

            var menu = await connection.QueryFirstOrDefaultAsync<LunchDTO>($@"
                SELECT 
	                Date, 
                    Menu 
                FROM Lunch
                WHERE Date > @Today
                ORDER BY Date", new
            {
                Today = DateTime.Now
            });

            // Checks if the menu is for today or tomorrow.
            if(menu.Date.Date == DateTime.Now.Date)
            {
                return $"Menuen i dag er {menu.Menu}";
            }
            return $"Menuen i morgen vil være {menu.Menu}";
        }
    }
}
