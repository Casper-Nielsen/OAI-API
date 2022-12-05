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
        private readonly Mock<IAnswerRepository> _answerRepositoryMock = new();
        private readonly Mock<IAIRepository> _aiRepositoryMock = new();
        private readonly Mock<ILocationRepository> _locationRepositoryMock = new();
        private readonly Mock<ILunchRepository> _lunchRepositoryMock = new();

        [SetUp]
        public void Setup()
        {
            //mock data for repositoried that uses a database
            _answerRepositoryMock
                .Setup(r => r.GetAnswerAsync(new string[1] {"test"}))
                .ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer", Status = Status.Active});
            _answerRepositoryMock
                .Setup(r => r.GetAnswerAsync(new string[4] {"this", "is", "a", "test"}))
                .ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer", Status = Models.Status.Active});
            
            _answerRepositoryMock
                .Setup(r => r.GetAnswerAsync(0))
                .ReturnsAsync(new AnswerDTO { AnswerId = 0, AnswerType = AnswerType.Static, AnswerValue = "test answer0", Status = Status.Active});
            _answerRepositoryMock
                .Setup(r => r.GetAnswerAsync(1))
                .ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer1", Status = Status.Active});
            _answerRepositoryMock
                .Setup(r => r.GetAnswerAsync(2))
                .ReturnsAsync(new AnswerDTO { AnswerId = 2, AnswerType = AnswerType.External, AnswerValue = "test answer2", Status = Status.Active});

            _aiRepositoryMock
                .Setup(r => r.GetAnswerAsync("this is a test"))
                .ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer", Status = Models.Status.Active, ValidKeywords = new string[] { "this", "is", "a", "test" } });
            _aiRepositoryMock
                .Setup(r => r.GetAnswerAsync("test"))
                .ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.Static, AnswerValue = "test answer", Status = Status.Active, ValidKeywords = new string[] { "test" } });
            _aiRepositoryMock
                .Setup(r => r.GetAnswerAsync("location test"))
                .ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.Location, AnswerValue = "test", Status = Status.Active, ValidKeywords = new string[] { "location", "test" } });
            _aiRepositoryMock
                .Setup(r => r.GetAnswerAsync("lunch test"))
                .ReturnsAsync(new AnswerDTO { AnswerId = 1, AnswerType = AnswerType.External, AnswerValue = "frokost", Status = Status.Active, ValidKeywords = new string[] { "lunch", "test" } });

            _locationRepositoryMock
                .Setup(r => r.GetDirectionsToLocationAsync("test"))
                .ReturnsAsync("test location");

            _lunchRepositoryMock
                .Setup(r => r.GetLunchMenuAsync())
                .ReturnsAsync("lunch menu");

            _answerService = new AnswerService(
                _answerRepositoryMock.Object,
                _aiRepositoryMock.Object,
                _locationRepositoryMock.Object,
                _lunchRepositoryMock.Object);
        }

        [TestCase(0)]
        [TestCase(1)]
        public async Task Get_GetWithAnswerId_shouldReturn(int id)
        {
            var answer = await _answerService.GetAnswerAsync(id);

            Assert.IsNotNull(answer);
            Assert.AreEqual(id, answer.AnswerId);
            Assert.AreEqual(AnswerType.Static, answer.Type);
        }

        [TestCase(2)]
        public async Task Get_GetWithAnswerId_shouldReturnExtendedAnswer(int id)
        {
            var answer = await _answerService.GetAnswerAsync(id);

            Assert.IsNotNull(answer);
            Assert.DoesNotThrow(() => { var extendedAnswer = (ExtendedAnswer)answer; });
            Assert.AreEqual(id, answer.AnswerId);
            Assert.AreEqual(AnswerType.External, answer.Type);
        }


        [TestCase(5)]
        public void Get_GetWithInvaildAnswerId_shouldThrow(int id)
        {
            Assert.ThrowsAsync<ArgumentException>(() => _answerService.GetAnswerAsync(id));
        }


        [TestCase("test")]
        [TestCase("this is a test")]
        public async Task Get_GetWithQuestion_shouldReturn(string question)
        {
            var answer = await _answerService.GetAnswerAsync(question);

            Assert.IsNotNull(answer);
            Assert.AreEqual(1, answer.AnswerId);
            Assert.AreEqual(AnswerType.Static, answer.Type);
        }

        [TestCase("location test")]
        public async Task Get_GetWithLocationQuestion_shouldReturn(string question)
        {
            var answer = await _answerService.GetAnswerAsync(question);

            Assert.IsNotNull(answer);
            Assert.AreEqual(1, answer.AnswerId);
            Assert.AreEqual("test location", answer.AnswerText);
            Assert.AreEqual(AnswerType.Location, answer.Type);
        }

        [TestCase("lunch test")]
        public async Task Get_GetWithLunchQuestion_shouldReturn(string question)
        {
            var answer = await _answerService.GetAnswerAsync(question);

            Assert.IsNotNull(answer);
            Assert.AreEqual(1, answer.AnswerId);
            Assert.AreEqual("lunch menu", answer.AnswerText);
            Assert.AreEqual(AnswerType.External, answer.Type);
        }

        [TestCase("hello")]
        public void Get_GetWithUnkownKeywords_shouldThrow(string question)
        {
            Assert.ThrowsAsync<ArgumentException>(() => _answerService.GetAnswerAsync(question));
        }
    }
}
