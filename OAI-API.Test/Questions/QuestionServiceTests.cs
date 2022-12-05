using Moq;
using NUnit.Framework;
using OAI_API.Models;
using OAI_API.Repositories;
using OAI_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI_API.Test.Questions
{
    [TestFixture]
    internal class QuestionServiceTests
    {
        private IQuestionService _questionService;
        private readonly Mock<IQuestionRepository> _questionRepository = new();
        private readonly List<Question> _defaultBasicQuestions = new();
        private readonly List<Question> _defaultFullQuestions = new();

        [SetUp]
        public void Setup()
        {
            //mock data for repositoried that uses a database
            _questionRepository
                .Setup(r => r.RegisterQuestionAsync(It.Is<QuestionDTO>(t => t.Question == "this is a test")))
                .ReturnsAsync(new QuestionDTO() { AnswerId = 1, Keywords = new string[] { "this", "test" }, Question = "this is a test", Status = QuestionStatus.proced, Id = 1});
            _questionRepository
                .Setup(r => r.RegisterQuestionAsync(It.Is<QuestionDTO>(t => t.Question == "this is 2nd test")))
                .ReturnsAsync(new QuestionDTO() { AnswerId = 1, Keywords = new string[] { "this", "test" }, Question = "this is 2nd test", Status = QuestionStatus.proced, Id = 2 });

            _questionRepository.Setup(r => r.GetQuestionAsync(1))
                .ReturnsAsync(new QuestionDTO() { AnswerId = 1, Keywords = new string[] { "this", "test" }, Question = "this is a test", Status = QuestionStatus.proced, Id = 1 });
            _questionRepository.Setup(r => r.GetQuestionAsync(2))
                .ReturnsAsync(new QuestionDTO() { AnswerId = 1, Keywords = new string[] { "this", "test" }, Question = "this is 2nd test", Status = QuestionStatus.proced, Id = 2 });

            _questionRepository.Setup(r => r.UpdateQuestionAsync(It.Is<QuestionDTO>(q => q.Id == 1 && q.Status == QuestionStatus.accepted)));
            _questionRepository.Setup(r => r.UpdateQuestionAsync(It.Is<QuestionDTO>(q => q.Id == 2 && q.Status == QuestionStatus.rejected)));

            _defaultBasicQuestions.Add(new Question(new QuestionDTO() { AnswerId = 1, Keywords = new string[] { "this", "test" }, Question = "this is a test" }, new Answer() { AnswerId = 1}));
            _defaultFullQuestions.Add(new Question(new QuestionDTO() { AnswerId = 1, Keywords = new string[] { "this", "test" }, Question = "this is a test", Status = QuestionStatus.proced, Id = 1 }, new Answer() { AnswerId = 1 }));
            _defaultBasicQuestions.Add(new Question(new QuestionDTO() { AnswerId = 1, Keywords = new string[] { "this", "test" }, Question = "this is 2nd test" }, new Answer() { AnswerId = 1 }));
            _defaultFullQuestions.Add(new Question(new QuestionDTO() { AnswerId = 1, Keywords = new string[] { "this", "test" }, Question = "this is 2nd test", Status = QuestionStatus.proced, Id = 2 }, new Answer() { AnswerId = 1 }));

            _questionService = new QuestionService(_questionRepository.Object);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task Get_GetQuestionAsync_shouldReturn(int id)
        {
            var question = await _questionService.GetQuestionAsync(id);

            var index = id - 1;

            Assert.AreEqual(_defaultFullQuestions[index].Status, question.Status);
            Assert.AreEqual(_defaultFullQuestions[index].Keywords, question.Keywords);
            Assert.AreEqual(_defaultFullQuestions[index].Text, question.Text);
            Assert.AreEqual(_defaultFullQuestions[index].Answer!.AnswerId, question.Answer!.AnswerId);
        }

        [TestCase(0)]
        [TestCase(1)]
        public async Task Get_RegisterQuestionAsync_shouldReturn(int index)
        {
            var question = await _questionService.RegisterQuestionAsync(_defaultBasicQuestions[index]);

            Assert.AreEqual(_defaultFullQuestions[index].Status, question.Status);
            Assert.AreEqual(_defaultFullQuestions[index].Keywords, question.Keywords);
            Assert.AreEqual(_defaultFullQuestions[index].Text, question.Text);
            Assert.AreEqual(_defaultFullQuestions[index].Answer!.AnswerId, question.Answer!.AnswerId);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Get_UpdateQuestionAsync_shouldNotThrow(int index)
        {
            var question = _defaultBasicQuestions[index];
            question.Status = index == 0 ? QuestionStatus.accepted : QuestionStatus.rejected;
            Assert.DoesNotThrowAsync(() => _questionService.UpdateQuestionAsync(question));
        }
    }
}
