namespace OAI_API.Services
{
    public interface ILocationService
    {
        /// <summary>
        /// Gets the directions to the location.
        /// </summary>
        /// <param name="location">The end location</param>
        /// <returns>The directions</returns>
        public Task<string> GetDirectionAsync(string location);
    }
}
