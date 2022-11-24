namespace OAI_API.Repositories
{
    public interface ILocationRepository
    {
        /// <summary>
        /// Gets the Directions to the given location
        /// </summary>
        /// <param name="location">The end location</param>
        /// <returns>Directions</returns>
        Task<string> GetDirectionsToLocationAsync(string location);
    }
}
