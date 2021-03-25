using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SiiConcatelPractice.Controllers;
using SiiConcatelPractice.Models;
using SiiConcatelPractice.Repositorys;
using SiiConcatelPractice.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace SiiConcatelPractice.Tests.APIs.Rebels
{
    public class RebelsApiTest
    {
        private readonly RebelsController _con;
        private readonly Mock<IRebelRepository> _mockRepo = new Mock<IRebelRepository>();
        private readonly Mock<ILogger<RebelsController>> _logger = new Mock<ILogger<RebelsController>>();
        public RebelsApiTest()
        {
            _con = new RebelsController(_mockRepo.Object, _logger.Object);
        }

        [Fact]
        public void Rebel_GetById_Successfully()
        {
            //Arrange
            var rebelId = 20;
            var name = "pepe";
            var planeta = "tierra";

            var rebelDto = new Rebel()
            {
                Id = rebelId,
                Name = name,
                Planet = planeta
            };
            _mockRepo.Setup(x => x.GetRebelById(rebelId)).Returns(rebelDto);

            //Act
            var rebel = _con.GetRebelById(rebelId);

            //ASSERT
            var actionResult = rebel.Result;
            var apiResult = (actionResult as OkObjectResult).Value as Rebel;
            Assert.NotNull(apiResult);
            Assert.Equal(rebelId, apiResult.Id);
        }
        [Fact]
        public void Post_Rebel_Successfully()
        {
            //Arrange
            var name = "UnitTest";
            var planet = "PlanetTest";

            var rebelDto = new Rebel()
            {
                Name = name,
                Planet = planet
            };
            var list = new RebelList()
            {
                Rebels = new List<Rebel>()
                {
                    rebelDto
                }
            };
            //Act
            var rebel = _con.Post(list);

            //ASSERT
            var actionResult = rebel.Result;
            var apiResult = (actionResult as OkObjectResult).Value as ApiResult;
            Assert.NotNull(apiResult);
        }


        [Fact]
        public void GetById_ShouldReturnNothing_WhenRebelDoesNotExist()
        {

            //Arrange

            _mockRepo.Setup(x => x.GetRebelById(It.IsAny<int>())).Returns(() => null);

            //Act
            var rebel = _con.GetRebelById(2);
            var actionResult = rebel.Result;
            var apiResult = (actionResult as BadRequestObjectResult).Value as Rebel;

            //ASSERT
            Assert.Null(apiResult);
        }

        [Fact]
        public void Delete_Rebel_Succesfully()
        {
            //Arrange
            var testId = 500;
            _mockRepo.Setup(r => r.GetRebelById(testId)).Returns(new Rebel { Id = testId });

            //Act
            var result = _con.Delete(testId);

            //ASSERT

            var actionResult = result.Result;
            Assert.IsType<OkObjectResult>(actionResult);
            var apiResult = (actionResult as OkObjectResult).Value as ApiResult;
            Assert.NotNull(apiResult);
            Assert.Equal("Rebel deleted correctly", apiResult.Message);
        }


        [Fact]
        public void Delete_Rebel_NotWork()
        {
            //Arrange
            var notFoundId = 500;

            //Act
            var result = _con.Delete(notFoundId);

            //ASSERT
            var actionResult = result.Result;
            Assert.IsType<BadRequestObjectResult>(actionResult);
            var apiResult = (actionResult as BadRequestObjectResult).Value as ApiResult;
            Assert.NotNull(apiResult);
            Assert.Equal($"The rebel with id {notFoundId} has not been found", apiResult.Message);
        }
       
    }

}
