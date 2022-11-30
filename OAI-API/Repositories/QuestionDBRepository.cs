using Dapper;
using OAI_API.Models;
using OAI_API.Shared;
using System.Data;

namespace OAI_API.Repositories
{
    public class QuestionDBRepository : IQuestionRepository
    {
        private readonly IDatabaseFactory _connectionFactory;

        public QuestionDBRepository(IDatabaseFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<QuestionDTO> RegisterQuestionAsync(QuestionDTO question)
        {
            var connection = await _connectionFactory.GetConnection();

            var questionId = await connection.QueryFirstOrDefaultAsync<int>(@"INSERT INTO 
                   OAI.PreviousQuestion (Question, AnswerId, Status) 
                   VALUES (@Question, @AnswerId, @Status);
                SELECT LAST_INSERT_ID()", new
                {
                    Question = question.Question,
                    AnswerId = question.AnswerId,
                    Status = QuestionStatus.proced
                });

            await AddKeywordBinding(connection, question.Keywords, questionId);

            question.Id= questionId;
            question.Status = QuestionStatus.proced;

            return question;
        }

        public async Task<QuestionDTO> GetQuestionAsync(int questionid)
        {
            var connection = await _connectionFactory.GetConnection();

            var question = await connection.QueryFirstOrDefaultAsync<QuestionDTO>(@"
                SELECT 
                    Question, 
                    Status, 
                    AnswerId,
                    Id
                FROM PreviousQuestion 
                WHERE Id = @Id", new
            {
                Id = questionid
            });

            question.Keywords = (await connection.QueryAsync<string>(@"
                SELECT 
	                k.Word
                FROM Question_Keyword as qk
                INNER JOIN Keyword as k 
                    ON k.Id = qk.KeywordId
                WHERE qk.QuestionId = @Id", new
            {
                Id = questionid
            })).ToArray();

            return question;
        }

        public async Task UpdateQuestionAsync(QuestionDTO question)
        {
            var connection = await _connectionFactory.GetConnection();

            await connection.ExecuteAsync(@"
                UPDATE PreviousQuestion
                SET 
                    Question = @Question,
                    Status = @Status,
                    AnswerId = @AnswerId
                WHERE Id = @Id", new
            {
                Question = question.Question,
                AnswerId = question.AnswerId,
                Status = question.Status,
                Id = question.Id
            });

            await AddKeywordBinding(connection, question.Keywords, question.Id);
        }

        /// <summary>
        /// Adds the missing keyword bindings
        /// </summary>
        /// <param name="connection">A open connection</param>
        /// <param name="keywords">Keywords to add</param>
        /// <param name="id">Question Id</param>
        private async Task AddKeywordBinding(IDbConnection connection, string[] keywords, int id)
        {
            if (keywords == null) return;

            foreach (string keyword in keywords)
            {
                await connection.ExecuteAsync(@"Call Add_keyword_to_question(@QuestionId, @Keyword);", new
                {
                    QuestionId = id,
                    Keyword = keyword
                });
            }
        }
    }
}
