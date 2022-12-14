using OAI_API.Models;
using OAI_API.Shared;
using Dapper;

namespace OAI_API.Repositories
{
    /// <summary>
    /// Controls the Answers in the database
    /// </summary>
    public class AnswerDBRepository : DBRepository, IAnswerRepository
    {
        public AnswerDBRepository(IDatabaseFactory connectionFactory) : base(connectionFactory) { }

        public async Task<AnswerDTO> GetAnswerAsync(int answerId)
        {
            var connection = await _connectionFactory.GetConnection();

            var answer = await connection.QueryFirstOrDefaultAsync<AnswerDTO>($@"
                SELECT 
                    Id AS AnswerId, 
                    AnswerType AS AnswerType, 
                    Text AS AnswerValue,
                    state AS Status
                FROM Answer 
                WHERE Id = @Id", new
            {
                Id = answerId
            });

            return answer;
        }

        public async Task<AnswerDTO> GetAnswerAsync(string[] answerKeyWords)
        {
            var connection = await _connectionFactory.GetConnection();

            var answer = await connection.QueryAsync<AnswerDTO>($@"
                SELECT 
                    ans.Id AS AnswerId, 
                    ans.AnswerType AS AnswerType, 
                    ans.Text AS AnswerValue,
                    ans.state AS Status,
                    SUM(akw.Weight) AS weight_sum 
                FROM OAI.Keyword AS kw
                        INNER JOIN OAI.Answer_Keyword AS akw ON kw.Id = akw.KeywordId
                        INNER JOIN OAI.Answer AS ans ON akw.AnswerId = ans.Id
                WHERE kw.word IN @Keywords
                    GROUP BY AnswerId
                    ORDER BY weight_sum DESC
                LIMIT 1;", new
            {
                Keywords = answerKeyWords
            });

            return answer.First();
        }
    }
}
