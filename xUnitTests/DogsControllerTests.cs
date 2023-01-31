using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTestTask.Controllers;
using WebApiTestTask.Dtos;
using WebApiTestTask.Entities;
using WebApiTestTask.Repositories;
using Xunit;

namespace xUnitTests
{
    public class DogsControllerTests
    {
        private readonly Mock<IDogsRepository> repositoryStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetDogAsync_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetDogAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Dog)null);
            var controller = new DogsController(repositoryStub.Object);

            // Act
            var result = await controller.GetDogAsync(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();

            //Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetDogAsync_WithExistingItem_ReturnsExpectedDog()
        {
            // Arrange
            var expectedDog = CreateRandomDog();
            repositoryStub.Setup(repo => repo.GetDogAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedDog);
            var controller = new DogsController(repositoryStub.Object);

            // Act
            var result = await controller.GetDogAsync(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(
                expectedDog, 
                options => options.ComparingByMembers<Dog>());

            //Assert.IsType<DogDto>(result.Value);
            //var dto = (result as ActionResult<DogDto>).Value;

            //Assert.Equal(expectedDog.Id, dto.Id);
            //Assert.Equal(expectedDog.Name, dto.Name);
            //Assert.Equal(expectedDog.Color, dto.Color);
            //Assert.Equal(expectedDog.Weight, dto.Weight);
            //Assert.Equal(expectedDog.TailLength, dto.TailLength);
        }

        [Fact]
        public async Task GetDogsAsync_WithExistingItem_ReturnsAllDogs()
        {
            // Arrange
            var expectedDogs = new Dog[10];
            for (int i = 0; i < 10; i++)
            {
                expectedDogs[i] = CreateRandomDog();
            }
            repositoryStub.Setup(repo => repo.GetOrderedDogsAsync("","",0,0))
                .ReturnsAsync(expectedDogs);
            var controller = new DogsController(repositoryStub.Object);

            // Act
            var actualDogs = await controller.GetDogsAsync("", "", 0, 0);

            // Assert
            actualDogs.Should().BeEquivalentTo(
                expectedDogs,
                options => options.ComparingByMembers<Dog>());
        }

        [Fact]
        public async Task CreateDogAsync_WithExistingItem_ReturnsCreatedItem()
        {
            // Arrange
            var dogToCreate = new CreateDogDto()
            {
                Name = Guid.NewGuid().ToString(),
                Color = Guid.NewGuid().ToString(),
                Weight = rand.Next(1, 1000),
                TailLength = rand.Next(1, 1000)
            };

            var controller = new DogsController(repositoryStub.Object);

            // Act
            var result = await controller.CreateDogAsync(dogToCreate);

            // Assert
            var createdDog = (result.Result as CreatedAtActionResult).Value as DogDto;
            dogToCreate.Should().BeEquivalentTo(
                createdDog,
                options => options.ComparingByMembers<DogDto>().ExcludingMissingMembers());

            createdDog.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateDogAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            var exsitingDog = CreateRandomDog();
            repositoryStub.Setup(repo => repo.GetDogAsync(It.IsAny<Guid>()))
                .ReturnsAsync(exsitingDog);

            var dogId = exsitingDog.Id;
            var dogToUpdate = new UpdateDogDto()
            {
                Name = Guid.NewGuid().ToString(),
                Color = Guid.NewGuid().ToString(),
                Weight = rand.Next(1, 1000),
                TailLength = rand.Next(1, 1000)
            };

            var controller = new DogsController(repositoryStub.Object);

            // Act
            var result = await controller.UpdateDogAsync(dogId, dogToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteDogAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            var exsitingDog = CreateRandomDog();
            repositoryStub.Setup(repo => repo.GetDogAsync(It.IsAny<Guid>()))
                .ReturnsAsync(exsitingDog);

            var controller = new DogsController(repositoryStub.Object);

            // Act
            var result = await controller.DeleteDogAsync(exsitingDog.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        private Dog CreateRandomDog()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Color = Guid.NewGuid().ToString(),
                Weight = rand.Next(1, 1000),
                TailLength = rand.Next(1, 1000)
            };
        }
    }
}
