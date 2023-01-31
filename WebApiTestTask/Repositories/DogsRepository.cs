using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApiTestTask.DataAccess;
using WebApiTestTask.Entities;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic;

namespace WebApiTestTask.Repositories
{
    public class DogsRepository : IDogsRepository
    {
        DogsContext db = new ();

        public async Task<IEnumerable<Dog>> GetDogsAsync()
        {
            return await db.Dogs.ToListAsync();
        }

        public async Task<IEnumerable<Dog>> GetOrderedDogsAsync(string sortAttribute, string sortOrder, int pageNumber, int pageSize)
        {
            var dogs = await GetDogsAsync();

            //Sorting
            if (sortOrder == "asc")
                dogs = dogs.OrderBy(sortAttribute);
            else if (sortOrder == "desc")
                dogs = dogs.OrderBy(sortAttribute).Reverse();

            //Pagination
            if (pageNumber > 0 && pageSize > 0)
                dogs = dogs.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return dogs;
        }

        public async Task<Dog> GetDogAsync(Guid id)
        {
            return await db.Dogs.SingleOrDefaultAsync(dog => dog.Id == id);
        }

        public async Task CreateDogAsync(Dog dog)
        {
            await db.Dogs.AddAsync(dog);
            await db.SaveChangesAsync();
        }

        public async Task UpdateDogAsync(Dog dog)
        {
            db.Dogs.Update(dog);
            await db.SaveChangesAsync();
        }

        public async Task DeleteDogAsync(Guid id)
        {
            var dog = db.Dogs.Find(id);
            db.Dogs.Remove(dog);
            await db.SaveChangesAsync();
        }
    }
}
