using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTestTask.Dtos
{
    public record DogDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Color { get; init; }
        public int TailLength { get; init; }
        public int Weight { get; init; }
    }
}
