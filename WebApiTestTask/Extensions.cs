using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestTask.Dtos;
using WebApiTestTask.Entities;

namespace WebApiTestTask
{
    public static class Extensions
    {
        public static DogDto AsDto(this Dog dog)
        {
            return new DogDto
            {
                Id = dog.Id,
                Name = dog.Name,
                Color = dog.Color,
                TailLength = dog.TailLength,
                Weight = dog.Weight
            };
        }
    }
}
