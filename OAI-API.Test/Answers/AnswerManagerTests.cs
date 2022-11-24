using Moq;
using NUnit.Framework;
using OAI_API.Manager;
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
    internal class AnswerManagerTests
    {
        private IAnwserManager _answerManager;

        [SetUp]
        public void Setup()
        {
            //mock data for repositoried that uses a database
            Mock<IAnswerRepository> _answerRepositoryMock = new Mock<IAnswerRepository>();

            _answerRepositoryMock.Setup(r => r.GetAnswerAsync(new string[1] { "test" })).ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer", Status = Status.Active });
            _answerRepositoryMock.Setup(r => r.GetAnswerAsync(new string[4] { "this", "is", "a", "test" })).ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer", Status = Models.Status.Active });

            _answerRepositoryMock.Setup(r => r.GetAnswerAsync(0)).ReturnsAsync(new AnswerDTO { AnswerId = 0, AnswerType = AnswerType.Static, AnswerValue = "test answer0", Status = Status.Active });
            _answerRepositoryMock.Setup(r => r.GetAnswerAsync(1)).ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer1", Status = Status.Active });
            _answerRepositoryMock.Setup(r => r.GetAnswerAsync(2)).ReturnsAsync(new AnswerDTO { AnswerId = 2, AnswerType = AnswerType.External, AnswerValue = "test answer2", Status = Status.Active });

            IAnswerService _answerService = new AnswerService(_answerRepositoryMock.Object);

            _answerManager = new AnswerManager(_answerService);
        }

        [TestCase("test")]
        [TestCase("this is a test")]
        public async Task LookUp_LookupWithKeywords_shouldReturn(string question)
        {
            var answer = await _answerManager.GetAnswer(question);

            Assert.IsNotNull(answer);
            Assert.AreEqual(1, answer.AnswerId);
            Assert.AreEqual(AnswerType.Static, answer.Type);
        }


        [TestCase("hello")]
        public void LookUp_LookupWithUnkownKeywords_shouldThrow(string question)
        {
            Assert.ThrowsAsync<ArgumentException>(() => _answerManager.GetAnswer(question));
        }
    }
}
