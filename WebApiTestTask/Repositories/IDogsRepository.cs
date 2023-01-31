using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTestTask.Entities;

namespace WebApiTestTask.Repositories
{
    public interface IDogsRepository
    {
        Task<IEnumerable<Dog>> GetDogsAsync();
        Task<IEnumerable<Dog>> GetOrderedDogsAsync(string sortAttribute, string sortOrder, int pageNumber, int pageSize);
        Task<Dog> GetDogAsync(Guid id);
        Task CreateDogAsync(Dog dog);
        Task UpdateDogAsync(Dog dog);
        Task DeleteDogAsync(Guid id);
    }
}