namespace OAI_API.Repositories
{
    public interface ILunchRepository
    {
        /// <summary>
        /// Gets the Lunch menu for the next lunch period 
        /// </summary>
        Task<string> GetLunchMenuAsync();
    }
}
