using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestTask.Dtos;
using WebApiTestTask.Entities;
using WebApiTestTask.Repositories;

namespace WebApiTestTask.Controllers
{
    [ApiController]
    [Route("dogs")]
    public class DogsController : ControllerBase
    {
        private readonly IDogsRepository repository;

        public DogsController(IDogsRepository repository)
        {
            this.repository = repository;
        }

        //GET /dogs
        [HttpGet]
        public async Task<IEnumerable<DogDto>> GetDogsAsync([FromQuery(Name = "attribute")] string sortAttribute = "Name", [FromQuery(Name = "order")] string sortOrder = "",
            [FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery(Name = "pageSize")] int pageSize = 100)
        {
            var dogsFromDb = (await repository.GetOrderedDogsAsync(sortAttribute, sortOrder, pageNumber, pageSize))
                             .Select(dog => dog.AsDto());
            return dogsFromDb;
        }

        //GET /dogs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DogDto>> GetDogAsync(Guid id)
        {
            var dogFromDb = await repository.GetDogAsync(id);

            if (dogFromDb is null)
                return NotFound();

            return dogFromDb.AsDto();
        }

        //POST /dogs
        [HttpPost]
        public async Task<ActionResult<DogDto>> CreateDogAsync(CreateDogDto dogDto)
        {
            if ((await repository.GetDogsAsync())
                .Any(dog => dog.Name == dogDto.Name))
                    return StatusCode(409, $"Dog with name '{dogDto.Name}' already exists.");

            Dog dog = new()
            {
                Id = Guid.NewGuid(),
                Name = dogDto.Name,
                Color = dogDto.Color,
                TailLength = dogDto.TailLength,
                Weight = dogDto.Weight
            };

            await repository.CreateDogAsync(dog);

            return CreatedAtAction(nameof(GetDogAsync), new { id = dog.Id }, dog.AsDto());
        }

        //PUT /dogs/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDogAsync(Guid id, UpdateDogDto dogDto)
        {
            if ((await repository.GetDogsAsync())
                .Any(dog => dog.Name == dogDto.Name && dog.Id != id))
                    return StatusCode(409, $"Dog with name '{dogDto.Name}' already exists.");

            var dogFromDb = await repository.GetDogAsync(id);

            if (dogFromDb is null)
            {
                return NotFound();
            }

            dogFromDb.Name = dogDto.Name;
            dogFromDb.Color = dogDto.Color;
            dogFromDb.TailLength = dogDto.TailLength;
            dogFromDb.Weight = dogDto.Weight;

            await repository.UpdateDogAsync(dogFromDb);

            return NoContent();
        }

        //DELETE /dogs/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDogAsync(Guid id)
        {
            var dogFromDb = await repository.GetDogAsync(id);

            if (dogFromDb is null)
            {
                return NotFound();
            }

            await repository.DeleteDogAsync(id);

            return NoContent();
        }
    }
}
