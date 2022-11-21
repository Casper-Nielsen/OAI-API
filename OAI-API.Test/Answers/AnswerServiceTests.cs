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

namespace OAI_API.Test.Answers
{
    [TestFixture]
    internal class AnswerServiceTests
    {
        private IAnswerService _answerService;
        private Mock<IAnswerRepository> _answerRepositoryMock = new Mock<IAnswerRepository>();

        [SetUp]
        public void Setup()
        {
            //mock data for repositoried that uses a database
            _answerRepositoryMock.Setup(r => r.GetAnswer(new string[1] {"test"})).ReturnsAsync(new DataAnswer { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer", Status = Status.Active});
            _answerRepositoryMock.Setup(r => r.GetAnswer(new string[4] {"this", "is", "a", "test"})).ReturnsAsync(new DataAnswer { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer", Status = Models.Status.Active});
            
            _answerRepositoryMock.Setup(r => r.GetAnswer(0)).ReturnsAsync(new DataAnswer { AnswerId = 0, AnswerType = AnswerType.Static, AnswerValue = "test answer0", Status = Status.Active});
            _answerRepositoryMock.Setup(r => r.GetAnswer(1)).ReturnsAsync(new DataAnswer { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer1", Status = Status.Active});
            _answerRepositoryMock.Setup(r => r.GetAnswer(2)).ReturnsAsync(new DataAnswer { AnswerId = 2, AnswerType = AnswerType.external, AnswerValue = "test answer2", Status = Status.Active});
            
            _answerService = new AnswerService(_answerRepositoryMock.Object);
        }

        [TestCase(0)]
        [TestCase(1)]
        public async Task LookUp_LookupAnswerId_shouldReturn(int id)
        {
            var answer = await _answerService.GetAnswer(id);

            Assert.IsNotNull(answer);
            Assert.AreEqual(id, answer.AnswerId);
            Assert.AreEqual(AnswerType.Static, answer.Type);
        }

        [TestCase(2)]
        public async Task LookUp_LookupAnswerId_shouldReturnExtendedAnswer(int id)
        {
            var answer = await _answerService.GetAnswer(id);

            Assert.IsNotNull(answer);
            Assert.DoesNotThrow(() => { var extendedAnswer = (ExtendedAnswer)answer; });
            Assert.AreEqual(id, answer.AnswerId);
            Assert.AreEqual(AnswerType.external, answer.Type);
        }


        [TestCase(5)]
        public void LookUp_LookupInvaildAnswerId_shouldThrow(int id)
        {
            Assert.ThrowsAsync<ArgumentException>(() => _answerService.GetAnswer(id));
        }


        [TestCase("test")]
        [TestCase("this is a test")]
        public async Task LookUp_LookupWithKeywords_shouldReturn(string question)
        {
            var answer = await _answerService.GetAnswer(question.Split(' '));

            Assert.IsNotNull(answer);
            Assert.AreEqual(1, answer.AnswerId);
            Assert.AreEqual(AnswerType.Static, answer.Type);
        }


        [TestCase("hello")]
        public void LookUp_LookupWithUnkownKeywords_shouldThrow(string question)
        {
            Assert.ThrowsAsync<ArgumentException>(() => _answerService.GetAnswer(question.Split(' ')));
        }
    }
}
